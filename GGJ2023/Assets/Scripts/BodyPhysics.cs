using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPhysics : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    private float moveIntensity;
    [SerializeField]
    private float moveIntensityDecay;
    [SerializeField]
    private float minDistToStop;

    [SerializeField]
    private float gravity;



    public ArmRope[] arms;

    private Vector3 targetPosition;
    private Rigidbody m_rb;

    [Header("Selection")]
    private Ray _ray;
    private RaycastHit _hit;
    public LayerMask raycastLayers;
    private ArmRope _grabbedArm;

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = transform.position;
        m_rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Find point for body to try to be at and add force towards it
        if(arms.Length > 0)
        {
            Vector3 averagePosition = new Vector3();
            foreach(ArmRope a in arms)
            {
                averagePosition += a.transform.position + Vector3.down * gravity / arms.Length / 2;
            }
            targetPosition = averagePosition / arms.Length;
        }
        else if(arms.Length == 1)
        {
            targetPosition = arms[0].transform.position + Vector3.down * Vector3.Distance(arms[0].transform.position, transform.position);
        }

        Vector3 moveForce = (targetPosition - transform.position) * moveIntensity;
        if (Vector3.Distance(targetPosition, transform.position) > minDistToStop)
        {
            m_rb.AddForce(moveForce, ForceMode.Impulse);
            m_rb.AddForce(Vector3.down * gravity);
        }
        m_rb.velocity /= moveIntensityDecay;


        // Add gravity


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && _grabbedArm == null)
        {
            _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(_ray, out _hit, 100, raycastLayers))
            {
                _grabbedArm = _hit.collider.GetComponent<ArmRope>();
            }
        }
        else if (Input.GetMouseButtonDown(0) && _grabbedArm != null)
        {
            _grabbedArm = null;
        }

        if (_grabbedArm != null)
        {
            var mousePosition = Input.mousePosition;
            mousePosition.z = Camera.main.transform.position.z;
            var mouseToWorldPoint = -Camera.main.ScreenToWorldPoint(mousePosition);
            _grabbedArm.transform.position = new Vector3(mouseToWorldPoint.x, mouseToWorldPoint.y, _grabbedArm.transform.position.z);
        }
    }
}
