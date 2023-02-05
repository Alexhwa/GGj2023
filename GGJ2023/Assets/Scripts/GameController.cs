using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController _instance;
    public static GameController Instance => _instance ? _instance : _instance = FindObjectOfType<GameController>();

    [SerializeField] private DOTweenAnimation startAnimation;
    [SerializeField] private GameObject playerObject;
    [SerializeField] private GameObject fourSidedWalls;

    [HideInInspector] public Player player;
    [HideInInspector] public Arena CurrentArena;
    public HashSet<GameColor.COLOR> CurrentColors = new HashSet<GameColor.COLOR>();

    private int playerHealth; 
    
    public void SetUpGame(GameLevel level)
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
        player = Instantiate(playerObject, Vector3.zero, Quaternion.identity).GetComponentInChildren<Player>();
        CurrentArena = FindObjectOfType<Arena>();
        CurrentArena.InitArena(level);
        StartCoroutine(StartGame(level));
    }

    public IEnumerator StartGame(GameLevel level)
    {
        startAnimation.transform.parent.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        CurrentArena.StartGameRounds(level.rounds);
    }

    public void ClearField()
    {
        if(CurrentArena != null) Destroy(CurrentArena.gameObject);
        if(player != null) Destroy(player.transform.parent.gameObject);
        CurrentColors.Clear();
    }

    
}
