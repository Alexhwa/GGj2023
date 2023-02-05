using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject armObject;
    [SerializeField] private Collider hitbox;

    [SerializeField] private BodyPhysics bodyPhysics;

    [Header("Selection")]
    private Ray _ray;
    private RaycastHit _hit;
    public LayerMask raycastLayers;
    private ArmRope _grabbedArm;

    public float grabbedArmFollowDistance;

    public void AddArm(Vector3 position)
    {
        var arm = Instantiate(armObject,transform.parent);
        arm.transform.rotation = Quaternion.LookRotation(arm.transform.right, position);
        var armScript = arm.GetComponentInChildren<ArmRope>();
        armScript.currentState = ArmRope.STATE.Unset;
        bodyPhysics.AttachArm(armScript);
        armScript.Launch((position - transform.position).normalized);
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _grabbedArm == null)
        {
            _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(_ray, out _hit, 100, raycastLayers))
            {
                var armScript = _hit.collider.GetComponent<ArmRope>();
                if (armScript.CanBeGrabbed()) {
                    armScript.SetCanBeGrabbed(false);
                    _grabbedArm = armScript;
                    bodyPhysics.DetachArm(_grabbedArm);
                    _grabbedArm.CurrentColor = GameColor.COLOR.None;
                }
                return;
            }
        }
        if (_grabbedArm != null)
        {
            var followVector = _grabbedArm.transform.localPosition;
            followVector.z = 0;
            followVector.y = 6;
            _grabbedArm.transform.localPosition = Vector3.Slerp(followVector, _grabbedArm.transform.localPosition, 0.35f);

            var mousePosition = Input.mousePosition;
            mousePosition.z = Camera.main.transform.position.z;
            var mouseToWorldPoint = -Camera.main.ScreenToWorldPoint(mousePosition);

            mousePosition = new Vector3(mouseToWorldPoint.x, mouseToWorldPoint.y, _grabbedArm.transform.position.z);
            var vectorToMouse = (mousePosition - transform.position);
            vectorToMouse.z = 0;
            vectorToMouse = vectorToMouse.normalized;

            Transform armTransform = _grabbedArm.transform.parent.parent;
            armTransform.up = Vector3.RotateTowards(armTransform.up, vectorToMouse, 0.6f, 0);
            if (Input.GetMouseButtonDown(0))
            {
                armTransform.up = Vector3.RotateTowards(armTransform.up, vectorToMouse, 8f, 0);
                _grabbedArm.Launch(vectorToMouse);
                bodyPhysics.AttachArm(_grabbedArm);
                _grabbedArm = null;
            }
        }
    }

    public void OnEnemyContact()
    {
        //TODO:Damage Animation
    }
    private void OnCollisionEnter(Collision other)
    {
        //TODO: Handle death when touching wall
    }
    public ArmRope GetRandomRope()
    {
        List<ArmRope> ropes_cpy = new List<ArmRope>(bodyPhysics.arms);
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
