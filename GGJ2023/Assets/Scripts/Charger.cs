using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charger : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float waitDuration;
    private float waitTimer;

    private Transform _playerBody;
    private GameColor.COLOR color;

    

    private enum MoveState
    {
        Wait, Aim, Charge
    }
    private MoveState moveState;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return null;
        StartCoroutine(Aim());
    }

    private IEnumerator Aim()
    {
        float timer = waitDuration;
        while (timer > 0)
        {
            transform.up = Vector3.RotateTowards(transform.up, _playerBody.transform.position - transform.position, 0.2f, 10);
            timer -= Time.deltaTime;
            yield return null;
        }
        StartCoroutine(Charge());
    }

    private IEnumerator Charge()
    {
        yield return null;
    }
}
