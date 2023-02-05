using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillVFX : MonoBehaviour
{
    [SerializeField] private float particleTimer;

    void Start() 
    {
        StartCoroutine(VFXTimer());
    }

    private IEnumerator VFXTimer()
    {
        yield return new WaitForSeconds(particleTimer);
        Destroy(gameObject);
    }
    
}
