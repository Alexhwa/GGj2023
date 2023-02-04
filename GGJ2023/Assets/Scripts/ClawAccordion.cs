using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawAccordion : MonoBehaviour
{
    [SerializeField] private GameObject claw;
    [SerializeField] private GameObject extender;
    private float distance;

    void Update()
    {
        LengthUpdate();
    }

    private void LengthUpdate()
    {
        distance = Vector3.Distance(transform.position, claw.transform.position);
        extender.transform.localScale = new Vector3(1, 2.965f*(distance), 1);
    }
}
