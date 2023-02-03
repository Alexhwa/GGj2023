using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField] private GameObject anchor;
    [SerializeField] [Tooltip("Collider marked as trigger")] private Collider selectionBox;
    [SerializeField] private LineRenderer lineRenderer;
    

    public enum STATE{Set,Unset}

    public STATE currentState;
    
    
    private GameColor.COLOR _color;
    private Vector3 rootPoint;
    private Vector3 anchorPoint;
    
    public GameColor.COLOR CurrentColor
    {
        get => _color;
        set
        {
            _color = value;
            //TODO: set the visual line color;
        }
    }

    public event Action<GameColor.COLOR> WallLinkListener; 
    

    public Rope InitRope(GameObject wallObject, int ropeNumber, int totalRopes)
    {
        //TODO: link start of rope with Player;
        //TODO: Do some logic using 2 given ints to evenly space the ropes on the wall;
        return this;
    }
    private void OnMouseDown()
    {
        currentState = STATE.Unset;
        _color = GameColor.COLOR.None;
    }

    private void PlaceRope(GameColor.COLOR color)
    {
        CurrentColor = color;
        WallLinkListener?.Invoke(color);
    }

    private void Update()
    {
        rootPoint = Player.Instance.transform.position;
        anchorPoint = anchor.transform.position;
        switch (currentState)
        {
            case STATE.Set:
                //TODO: Pull player
                break;
            case STATE.Unset:
                //TODO: Follow mouse
                break;
        }
        lineRenderer.SetPosition(0,rootPoint);
        lineRenderer.SetPosition(1,anchorPoint);
    }
}
