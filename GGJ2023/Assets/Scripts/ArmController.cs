using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    public LayerMask raycastLayers;
    private EnemyController grabbedArm;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && grabbedArm == null)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100, raycastLayers))
            {
                grabbedArm = hit.collider.gameObject.GetComponent<EnemyController>();
                grabbedArm.Detach();
            }
        }
        else if(Input.GetMouseButtonDown(0) && grabbedArm != null)
        {
            grabbedArm.Attach();
            grabbedArm = null;
        }

        if (grabbedArm != null)
        {
            var mousePosition = Input.mousePosition;
            mousePosition.z = Camera.main.transform.position.z;
            var mouseToWorldPoint = -Camera.main.ScreenToWorldPoint(mousePosition);
            grabbedArm.transform.position = new Vector3(mouseToWorldPoint.x, mouseToWorldPoint.y + 1, grabbedArm.transform.position.z);
        }
    }
}
