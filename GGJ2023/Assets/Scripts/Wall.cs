using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField][Tooltip("Indexed at 0. Starts at the top wall, increases clockwise. Starts at top-left wall if top is a point instead of an edge.")] 
    private int wallNumber;
    
    public Collider clickArea;
    [SerializeField] private GameColor.COLOR color;
    

    [SerializeField] private GameObject ropeObject;
    private List<Rope> _connectedRopes = new List<Rope>();

    public void InitWall(GameColor.COLOR startColor, int ropes)
    {
        color = startColor;
        //TODO: Recolor wall
        for (int i = 0; i < ropes; i++)
        {
            Rope rope = Instantiate(ropeObject).GetComponent<Rope>();
            _connectedRopes.Add(rope.InitRope(gameObject, i, ropes));
        }

        clickArea.enabled = false;
    }
    
    public void OnMouseDown()
    {
        
    }

    public GameColor.COLOR AddRope(Rope rope)
    {
        if (_connectedRopes.Contains(rope)) throw new Exception("Wall.cs: Tried to add duplicate rope.");
        _connectedRopes.Add(rope);
        return color;
    }
    
}

[CustomEditor(typeof(Wall))]
public class WallEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        //TODO: Change the wall color here
    }
}
