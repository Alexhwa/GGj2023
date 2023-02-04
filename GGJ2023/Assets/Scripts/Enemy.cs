using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameColor.COLOR color;

    [SerializeField][Tooltip("percent traveled/s")] private float speed;
    
    [Range(0,100)]private float _percentTraveled;
    private ArmRope _attachedArmRope;

    private void Start()
    {
        _attachedArmRope = Player.Instance.GetRandomRope();
        color = GameColor.RandomColorExcluding(color);
        //TODO Set enemy color
        //TODO: attach to the rope object
        _attachedArmRope.WallLinkListener += TryKill;
    }

    private void TryKill(GameColor.COLOR c)
    {
        if (color == c) Kill();
    }

    private void Kill()
    {
        _attachedArmRope.WallLinkListener -= TryKill;
        //TODO: Spawn Death Object
        Destroy(gameObject);
    }
    
    private void Update()
    {
        _percentTraveled += speed * Time.deltaTime;
    }
}
