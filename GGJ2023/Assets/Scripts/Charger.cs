using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charger : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float waitDuration;
    [SerializeField]
    private float aimDuration;
    [SerializeField]
    private float aimMoveSpeed;

    private float waitTimer;

    private GameColor.COLOR color;
    private Rigidbody m_rb;
    

    private enum MoveState
    {
        Wait, Aim, Charge
    }
    private MoveState moveState;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            print(other.gameObject.name);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Wall"))
        {
            m_rb.velocity = Vector3.zero;
            m_rb.isKinematic = true;
            StartCoroutine(Wait());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        float timer = waitDuration;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
        StartCoroutine(Aim());
    }

    private IEnumerator Aim()
    {
        float timer = aimDuration;
        while (timer > 0)
        {
            transform.up = Vector3.RotateTowards(transform.up, GameController.Instance.player.transform.position - transform.position, 0.1f, 10);
            transform.position += transform.up * aimMoveSpeed * Time.deltaTime * (timer / aimDuration);
            timer -= Time.deltaTime;
            yield return null;
        }
        StartCoroutine(Charge());
    }

    private IEnumerator Charge()
    {
        m_rb.isKinematic = false;

        var chargeDirection = (GameController.Instance.player.transform.position - transform.position).normalized;
        m_rb.velocity = chargeDirection * moveSpeed;
        yield return null;
    }

    private void Die()
    {

    }
}
