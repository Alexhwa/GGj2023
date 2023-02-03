using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    private static Player _instance;
    public static Player Instance => _instance ? _instance : _instance = FindObjectOfType<Player>();
    [SerializeField] private Collider hitbox;

    private List<Rope> _ropes;

    public Rope GetRandomRope()
    {
        List<Rope> ropes_cpy = new List<Rope>(_ropes);
        Rope rope = null;
        while (ropes_cpy.Count > 0)
        {
            rope = ropes_cpy[Random.Range(0, ropes_cpy.Count)];
            if (rope.currentState != Rope.STATE.Unset) break;
            ropes_cpy.Remove(rope);
        }
        if (rope == null) throw new NullReferenceException();
        return rope;
    }
    
    private void Start()
    {
        //For Debug:
        _ropes = new List<Rope>(FindObjectsOfType<Rope>());
    }

    private void OnCollisionEnter(Collision other)
    {
        //TODO: Handle death when touching wall
    }
}
