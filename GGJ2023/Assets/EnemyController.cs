using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed;
    private SpringJoint m_spring;
    private float springyness;

    public GameObject body;
    // Start is called before the first frame update
    void Start()
    {
        m_spring = GetComponent<SpringJoint>();
        springyness = m_spring.spring;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //Debug
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        transform.position += new Vector3(horizontal, vertical, 0) * moveSpeed * Time.deltaTime;
    }

    public void Detach()
    {
        m_spring.spring = 0;
    }
    public void Attach()
    {
        m_spring.spring = springyness;
        m_spring.connectedAnchor = (body.transform.position / 1.2f + transform.position) / 2;
    }
}
