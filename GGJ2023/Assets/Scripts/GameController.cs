using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController _instance;
    public static GameController Instance => _instance ? _instance : _instance = FindObjectOfType<GameController>();

    [SerializeField] private GameObject playerObject;
    [SerializeField] private GameObject fourSidedWalls;

    public Arena CurrentArena;
    public HashSet<GameColor.COLOR> CurrentColors = new HashSet<GameColor.COLOR>();
    
    public void StartGame(GameLevel level)
    {
        switch (level.walls.Count)
        {
            case 4:
                Instantiate(fourSidedWalls, Vector3.zero, Quaternion.identity);
                break;
            
        
        
            default:
                throw new Exception("GameController.cs: Invalid walls in Game Level: " + level.name);
                break;
        }
        Instantiate(playerObject, Vector3.zero, Quaternion.identity);
        CurrentArena = FindObjectOfType<Arena>();
        CurrentArena.InitArena(level);
        
    }


    
}
