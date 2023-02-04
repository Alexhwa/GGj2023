using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArmRope : MonoBehaviour
{
    [SerializeField] private GameObject anchor;
    [SerializeField] [Tooltip("Collider marked as trigger")] private Collider selectionBox;
    [SerializeField] private LineRenderer lineRenderer;

    private Rigidbody m_rb;
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
            lineRenderer.material.color = GameColor.GetColor(value);
        }
    }

    public event Action<GameColor.COLOR> WallLinkListener;
    private void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        lineRenderer.material = new Material(lineRenderer.material);
    }
    public ArmRope InitRope(GameObject wallObject, int ropeNumber, int totalRopes)
    {
        //TODO: link start of rope with Player;
        //TODO: Do some logic using 2 given ints to evenly space the ropes on the wall;
        return this;
    }
    private void OnMouseDown()
    {
        currentState = STATE.Set;
        _color = GameColor.COLOR.None;
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (currentState == STATE.Unset)
        {
            currentState = STATE.Set;
            m_rb.velocity = Vector3.zero;
            m_rb.isKinematic = true;
            PlaceRope(collision.gameObject.GetComponent<Wall>().color);
        }
    }

    private void PlaceRope(GameColor.COLOR color)
    {
        CurrentColor = color;
        WallLinkListener?.Invoke(color);
    }
    

    private void FixedUpdate()
    {
        rootPoint = Player.Instance.transform.position;
        anchorPoint = anchor.transform.position;
        switch (currentState)
        {
            case STATE.Set:
                
                break;
            case STATE.Unset:
                break;
        }
        lineRenderer.SetPosition(0,rootPoint);
        lineRenderer.SetPosition(1,anchorPoint);
    }

    public void Launch(Vector3 direction)
    {
        m_rb.isKinematic = false;
        m_rb.velocity = direction * moveSpeed;
        currentState = STATE.Unset;
    }
}
