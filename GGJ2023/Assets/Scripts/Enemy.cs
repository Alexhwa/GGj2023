using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameColor.COLOR color;

    [SerializeField][Tooltip("percent traveled/s")] private float speed;
    
    [Range(0,100)]private float _percentTraveled;
    private Rope _attachedRope;

    private void Start()
    {
        _attachedRope = Player.Instance.GetRandomRope();
        color = GameColor.RandomColorExcluding(color);
        //TODO Set enemy color
        //TODO: attach to the rope object
        _attachedRope.WallLinkListener += TryKill;
    }

    private void TryKill(GameColor.COLOR c)
    {
        if (color == c) Kill();
    }

    private void Kill()
    {
        _attachedRope.WallLinkListener -= TryKill;
        //TODO: Spawn Death Object
        Destroy(gameObject);
    }
    
    private void Update()
    {
        _percentTraveled += speed * Time.deltaTime;
    }
}
