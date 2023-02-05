using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawAccordion : MonoBehaviour
{
    [SerializeField] private GameObject claw;
    [SerializeField] private GameObject clawBase;
    [SerializeField] private GameObject extender;

    [SerializeField] private float SCALE_VALUE = 12;
    private float distance;
    private Vector3 InitialScale;

    void Start()
    {
        InitialScale = transform.localScale;
        LengthUpdate();
    }

    void LateUpdate()
    {
        //if (claw.transform.hasChanged || clawBase.transform.hasChanged)
        //{
            LengthUpdate();
        //}
    }

    private void LengthUpdate()
    {
        distance = Vector3.Distance(clawBase.transform.position, claw.transform.position);
        transform.localScale = new Vector3(InitialScale.x, (SCALE_VALUE*distance/2f), InitialScale.z);

        Vector3 middlePoint = (claw.transform.position + clawBase.transform.position)/2f;
        transform.position = middlePoint;

        Vector3 rotationDirection = (clawBase.transform.position - claw.transform.position);
        transform.up = rotationDirection;

        //extender.transform.localScale = new Vector3(1, SCALE_VALUE * (distance), 1);
    }
}
