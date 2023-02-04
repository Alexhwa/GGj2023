using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField][Tooltip("Indexed at 0. Starts at the top wall, increases clockwise. Starts at top-left wall if top is a point instead of an edge.")] 
    private int wallNumber;
    [SerializeField] public GameColor.COLOR color;

    [SerializeField] private MeshRenderer meshRenderer;
    //private List<ArmRope> _connectedRopes = new List<ArmRope>();

    private void Awake()
    {
        
    }

    public void InitWall(GameColor.COLOR startColor, int ropes)
    {
        color = startColor;
        meshRenderer.material = new Material(meshRenderer.material);
        meshRenderer.material.color = GameColor.GetColor(color);
        for (int i = 0; i < ropes; i++)
        {
            
            Player.Instance.AddArm(transform.position);
            //ArmRope armRope = Instantiate(ropeObject).GetComponent<ArmRope>();
            //_connectedRopes.Add(armRope.InitRope(gameObject, i, ropes));
        }
    }
    
    public void OnMouseDown()
    {
        
    }

    public GameColor.COLOR AddRope(ArmRope armRope)
    {
        //if (_connectedRopes.Contains(armRope)) throw new Exception("Wall.cs: Tried to add duplicate rope.");
        //_connectedRopes.Add(armRope);
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
