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

    public SkinnedMeshRenderer mesh;
    public List<Material> materials;
    private GameColor.COLOR color;
    private Rigidbody m_rb;

    private enum MoveState
    {
        Wait, Aim, Charge
    }
    private MoveState moveState;
    private MoveState currentState {
        get => moveState;
        set 
        { 
            moveState = value;
        }
    }

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
            if (collision.gameObject.GetComponent<Wall>().color == color) Kill();
            else
            {
                m_rb.velocity = Vector3.zero;
                m_rb.isKinematic = true;
                StartCoroutine(Wait());
            }
        }
    }

    private void Kill()
    {
        //TODO:Death Object
        Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        color = GameColor.RandomColorExcluding(GameColor.COLOR.Purple);
        mesh.material = materials[(int)color];
        m_rb = GetComponent<Rigidbody>();
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        currentState = MoveState.Wait;
        float timer = waitDuration;
        while (timer > 0 || GameController.Instance.player == null)
        {
            m_rb.velocity = Vector3.zero;
            timer -= Time.deltaTime;
            yield return null;
        }
        StartCoroutine(Aim());
    }

    private IEnumerator Aim()
    {
        currentState = MoveState.Aim;
        float timer = aimDuration;
        while (timer > 0)
        {
            m_rb.velocity = Vector3.zero;
            var towardsPlyerVector = (GameController.Instance.player.transform.position - transform.position).normalized;
            transform.up = Vector3.RotateTowards(transform.up, towardsPlyerVector, 0.1f, 10);
            /*if(transform.eulerAngles.z > 0 || transform.eulerAngles.z < -180)
            {
                transform.forward = Vector3.forward;
            }
            else
            {
                transform.forward = -Vector3.forward;
            }*/
            transform.position += towardsPlyerVector * aimMoveSpeed * Time.deltaTime * (timer / aimDuration);
            timer -= Time.deltaTime;
            yield return null;
        }
        StartCoroutine(Charge());
    }

    private IEnumerator Charge()
    {
        currentState = MoveState.Charge;
        m_rb.isKinematic = false;

        var chargeDirection = (GameController.Instance.player.transform.position - transform.position).normalized;
        m_rb.velocity = chargeDirection * moveSpeed;
        yield return null;
    }

    private void Die()
    {

    }
}
