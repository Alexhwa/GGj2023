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
    [SerializeField] private GameObject endAnimation;
    [SerializeField] private GameObject playerObject;
    [SerializeField] private GameObject fourSidedWalls;

    [HideInInspector] public Player player;
    [HideInInspector] public Arena CurrentArena;
    
    public HashSet<GameColor.COLOR> CurrentColors = new HashSet<GameColor.COLOR>();

    public event Action<int> HealthChangedListener;
    public event Action<bool> GameOverListener;
    public const int maxHealth = 3;
    private int playerHealth;
    private GameLevel currentLevel;
    
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
        playerHealth = maxHealth;
        HealthChangedListener?.Invoke(maxHealth);
        CurrentArena = FindObjectOfType<Arena>();
        currentLevel = level;
        CurrentArena.InitArena(level);
        StartCoroutine(StartGame(level));
    }

    public IEnumerator StartGame(GameLevel level)
    {
        if(GameManager.Instance != null) GameManager.Instance.musicManager.PlayMain();
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

    public void TakeDamage()
    {
        playerHealth -= 1;
        HealthChangedListener?.Invoke(playerHealth);
        if(playerHealth <= 0) StopGame(false);
    }

    private void StopGame(bool win)
    {
        Time.timeScale = 0;
        endAnimation.SetActive(true);
        endAnimation.GetComponent<DOTweenAnimation>().DOPlay();
        if (win)
        {
            
        }
        else
        {
            endAnimation.transform.GetChild(0).gameObject.SetActive(true);
        }
        GameOverListener?.Invoke(win);
    }

    public void RestartGame()
    {
        GameManager.Instance.LoadLevel(currentLevel);
    }
    
    public void GoBackToTitle()
    {
        GameManager.Instance.LoadTitle();
    }
    
    public void GoBackToLevelSelect()
    {
        GameManager.Instance.LoadLevelSelect();
    }
    
}
