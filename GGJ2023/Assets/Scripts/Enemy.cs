using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameColor.COLOR color = GameColor.COLOR.None;

    [SerializeField][Tooltip("percent traveled/s")] private float speed;
    
    [Range(0,100)]private float _percentTraveled;
    private ArmRope _attachedArmRope;

    private void Start()
    {
        _attachedArmRope = Player.Instance.GetRandomRope();
        //color = GameColor.RandomColorExcluding(color);
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
        transform.position =
            Vector3.Lerp(_attachedArmRope.anchorPoint, _attachedArmRope.rootPoint, _percentTraveled / 100);
        
        _percentTraveled += speed * Time.deltaTime;
    }
}
