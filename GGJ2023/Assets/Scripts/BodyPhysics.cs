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



    public List<ArmRope> arms = new List<ArmRope>();

    private Vector3 targetPosition;
    private Rigidbody m_rb;

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
        if(arms.Count > 1)
        {
            Vector3 averagePosition = new Vector3();
            foreach(ArmRope a in arms)
            {
                averagePosition += a.transform.position + Vector3.down * gravity / arms.Count / 2;
            }
            targetPosition = averagePosition / arms.Count;
        }
        /*else if(arms.Count == 1)
        {
            targetPosition = arms[0].transform.position + Vector3.down;
        }*/

        Vector3 moveForce = (targetPosition - transform.position) * moveIntensity;
        /*if (arms.Count == 1)
        {
            Vector3 perpMotionVector = Vector3.Cross((targetPosition - transform.position), Vector3.forward);
            moveForce = (perpMotionVector + (targetPosition - transform.position)) * moveIntensity;
            if (targetPosition.x < transform.position.x) moveForce *= -1;
        }*/

        if (Vector3.Distance(targetPosition, transform.position) > minDistToStop)
        {
            m_rb.AddForce(moveForce, ForceMode.Impulse);
            m_rb.AddForce(Vector3.down * gravity);
        }
        m_rb.velocity /= moveIntensityDecay;
    }

    public void DetachArm(ArmRope armRef)
    {
        arms.Remove(armRef);
    }

    public void AttachArm(ArmRope armRef)
    {
        arms.Add(armRef);
    }
}
