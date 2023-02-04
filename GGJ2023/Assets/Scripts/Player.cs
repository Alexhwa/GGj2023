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

    private List<ArmRope> _ropes;

    public ArmRope GetRandomRope()
    {
        List<ArmRope> ropes_cpy = new List<ArmRope>(_ropes);
        ArmRope armRope = null;
        while (ropes_cpy.Count > 0)
        {
            armRope = ropes_cpy[Random.Range(0, ropes_cpy.Count)];
            if (armRope.currentState != ArmRope.STATE.Unset) break;
            ropes_cpy.Remove(armRope);
        }
        if (armRope == null) throw new NullReferenceException();
        return armRope;
    }
    
    private void Start()
    {
        //For Debug:
        _ropes = new List<ArmRope>(FindObjectsOfType<ArmRope>());
    }

    private void OnCollisionEnter(Collision other)
    {
        //TODO: Handle death when touching wall
    }
}
