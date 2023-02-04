using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugScript : MonoBehaviour
{
    [SerializeField] private GameLevel level;
    [SerializeField] private GameObject enemy;
    
    public void SpawnRandomEnemy()
    {
        Instantiate(enemy);
    }

    public void StartGameDebug()
    {
        GameController.Instance.CurrentArena = FindObjectOfType<Arena>();
        GameController.Instance.CurrentArena.InitArena(level);
    }
}
