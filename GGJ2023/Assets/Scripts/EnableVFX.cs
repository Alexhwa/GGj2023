using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableVFX : MonoBehaviour
{
    [SerializeField] private float particleTimer;

    void OnEnable() 
    {
        StartCoroutine(VFXTimer());
    }

    private IEnumerator VFXTimer()
    {
        yield return new WaitForSeconds(particleTimer);
        gameObject.SetActive(false);
    }
}
