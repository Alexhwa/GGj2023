using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    private static Player _instance;
    public static Player Instance => _instance ? _instance : _instance = FindObjectOfType<Player>();
    [SerializeField] private Collider hitbox;

    private BodyPhysics bodyPhysics;

    private List<ArmRope> _ropes;

    [Header("Selection")]
    private Ray _ray;
    private RaycastHit _hit;
    public LayerMask raycastLayers;
    private ArmRope _grabbedArm;

    public float grabbedArmFollowDistance;
    
    private void Start()
    {
        //For Debug:
        _ropes = new List<ArmRope>(FindObjectsOfType<ArmRope>());
        bodyPhysics = GetComponent<BodyPhysics>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _grabbedArm == null)
        {
            _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(_ray, out _hit, 100, raycastLayers))
            {
                var armScript = _hit.collider.GetComponent<ArmRope>();
                if (armScript.currentState == ArmRope.STATE.Set) {
                    _grabbedArm = armScript;
                    bodyPhysics.DetachArm(_grabbedArm);
                    _grabbedArm.CurrentColor = GameColor.COLOR.None;
                }
                return;
            }
        }
        if (_grabbedArm != null)
        {
            var mousePosition = Input.mousePosition;
            mousePosition.z = Camera.main.transform.position.z;
            var mouseToWorldPoint = -Camera.main.ScreenToWorldPoint(mousePosition);

            mousePosition = new Vector3(mouseToWorldPoint.x, mouseToWorldPoint.y, _grabbedArm.transform.position.z);
            var vectorToMouse = mousePosition - transform.position;
            _grabbedArm.transform.position = transform.position + vectorToMouse.normalized * grabbedArmFollowDistance;

            if (Input.GetMouseButtonDown(0))
            {
                _grabbedArm.Launch(vectorToMouse);
                bodyPhysics.AttachArm(_grabbedArm);
                _grabbedArm = null;
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        //TODO: Handle death when touching wall
    }
    public ArmRope GetRandomRope()
    {
        List<ArmRope> ropes_cpy = new List<ArmRope>(_ropes);
        ArmRope armRope = null;
        while (ropes_cpy.Count > 0)
        {
            armRope = ropes_cpy[Random.Range(0, ropes_cpy.Count)];
            if (armRope.currentState != ArmRope.STATE.Unset) break;
            ropes_cpy.Remove(armRope);
        }
        if (armRope == null) throw new NullReferenceException();
        return armRope;
    }
}
