using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController _instance;
    public static GameController Instance = _instance ? _instance : _instance = FindObjectOfType<GameController>();

    [SerializeField] private GameObject fourSidedWalls;

    public Arena CurrentArena;
    public HashSet<GameColor.COLOR> CurrentColors = new HashSet<GameColor.COLOR>();
    
    public void StartGame(GameLevel level)
    {
        switch (level.walls.Count)
        {
            case 4:
                //TODO: Spawn fourSidedWalls Object
                break;
            
        
        
            default:
                throw new Exception("GameController.cs: Invalid walls in Game Level: " + level.name);
                break;
        }
        CurrentArena = FindObjectOfType<Arena>();
        CurrentArena.InitArena(level);
    }
}
