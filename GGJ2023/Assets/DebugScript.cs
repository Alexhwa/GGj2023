using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugScript : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    
    public void SpawnRandomEnemy()
    {
        Instantiate(enemy);
    }
}
