using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameColor.COLOR color = GameColor.COLOR.None;

    [SerializeField] private SpriteRenderer bodySprite;
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private GameObject enemyDeathVFX;
    [SerializeField][Tooltip("percent traveled/s")] private float speed;
    
    [Range(0,100)]private float _percentTraveled;
    [SerializeField] private ArmRope _attachedArmRope;

    private void Start()
    {
        _attachedArmRope = GameController.Instance.player.GetRandomRope();
        color = GameColor.RandomColorExcluding(color);
        bodySprite.color = GameColor.GetColor(color);
        particleSystem.startColor = GameColor.GetColor(color);
        _attachedArmRope.WallLinkListener += TryKill;
    }

    private void TryKill(GameColor.COLOR c)
    {
        if (color == c) Kill();
    }

    private void Kill()
    {
        _attachedArmRope.WallLinkListener -= TryKill;
        Instantiate(enemyDeathVFX, transform.position, Quaternion.identity);
        //TODO: Spawn Death Object
        Destroy(gameObject);
    }
    
    private void Update()
    {
        transform.position =
            Vector3.Lerp(_attachedArmRope.anchorPoint, _attachedArmRope.rootPoint, _percentTraveled / 100);
        
        if (_percentTraveled >= 100)
        {
            GameController.Instance.player.OnEnemyContact();
            Kill();
        }
        _percentTraveled = Math.Min( _percentTraveled + speed * Time.deltaTime, 100);
    }
}
