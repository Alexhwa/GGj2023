using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class ArmRope : MonoBehaviour
{
    [SerializeField] private GameObject anchor;
    [SerializeField] [Tooltip("Collider marked as trigger")] private Collider selectionBox;
    //[SerializeField] private LineRenderer lineRenderer;

    [SerializeField] private Rigidbody m_rb;
    [SerializeField] private GameObject clawVFX;
    [SerializeField] private float flashTimer;
    [SerializeField] private Material normalMaterial;
    [SerializeField] private MeshRenderer[] clawMeshRenderers;
    [SerializeField] private Material[] clawColorMaterials;
    [SerializeField] public AudioSource extendSound;
    [SerializeField] public AudioSource contractSound;
    public float moveSpeed;

    public enum STATE{Set,Unset}

    public STATE currentState;
    
    
    private GameColor.COLOR _color;
    public Vector3 rootPoint;
    public Vector3 anchorPoint;
    
    public GameColor.COLOR CurrentColor
    {
        get => _color;
        set
        {
            _color = value;
            //lineRenderer.material.color = GameColor.GetColor(value);
        }
    }

    public Animator _anim;

    public event Action<GameColor.COLOR> WallLinkListener;
    
    public ArmRope InitRope(GameObject wallObject, int ropeNumber, int totalRopes)
    {
        //TODO: link start of rope with Player;
        //TODO: Do some logic using 2 given ints to evenly space the ropes on the wall;
        return this;
    }
    public void OnGrab()
    {
        _color = GameColor.COLOR.None;
        contractSound.Play();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (currentState == STATE.Unset && collision.gameObject.tag.Equals("Wall"))
        {
            currentState = STATE.Set;
            m_rb.velocity = Vector3.zero;
            m_rb.isKinematic = true;
            PlaceRope(collision.gameObject.GetComponent<Wall>().color);
            clawVFX.SetActive(true);
            StartCoroutine(FlashWhenGrabbing(clawColorMaterials[(int)collision.gameObject.GetComponent<Wall>().color]));
            _anim.SetBool("Closed", true);
        }
    }

    private void PlaceRope(GameColor.COLOR color)
    {
        CurrentColor = color;
        WallLinkListener?.Invoke(color);
    }
    
    //Gamecolor.COLOR
    //red, yellow, green, blue, pink, purple

    private void FixedUpdate()
    {
        rootPoint = GameController.Instance.player.transform.position;
        anchorPoint = anchor.transform.position;
        switch (currentState)
        {
            case STATE.Set:
                
                break;
            case STATE.Unset:
                break;
        }
        //lineRenderer.SetPosition(0,rootPoint);
        //lineRenderer.SetPosition(1,anchorPoint);
    }

    public void Launch(Vector3 direction, bool mute = false)
    {
        m_rb.isKinematic = false;
        m_rb.velocity = direction * moveSpeed;
        currentState = STATE.Unset;
        _anim.SetBool("Closed", false);
        if(!mute)extendSound.Play();
    }

    public bool CanBeGrabbed()
    {
        return currentState == STATE.Set;
    }
    public void SetCanBeGrabbed(bool value)
    {
        currentState = (value) ? STATE.Set : STATE.Unset;
    }
    
    private IEnumerator FlashWhenGrabbing(Material flashMaterial)
    {
        foreach (MeshRenderer i in clawMeshRenderers)
        {
            i.material = flashMaterial;
        }

        yield return new WaitForSeconds(flashTimer);

        foreach (MeshRenderer i in clawMeshRenderers)
        {
            i.material = normalMaterial;
        }
    }
}
