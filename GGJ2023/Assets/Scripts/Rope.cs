using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField] [Tooltip("Collider marked as trigger")] private Collider selectionBox;
    
    public enum STATE{Set,Unset}

    public STATE currentState;

    private GameColor.COLOR _color;
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
        switch (currentState)
        {
            case STATE.Set:
                //TODO: Pull player
                break;
            case STATE.Unset:
                //TODO: Follow mouse
                break;
        }
    }
}
