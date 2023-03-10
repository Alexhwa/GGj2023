using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private static GameController _instance;
    public static GameController Instance => _instance ? _instance : _instance = FindObjectOfType<GameController>();


    [SerializeField] private AudioSource damageSource;
    [SerializeField] private AudioSource winSource;
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

    private bool endSelectionPressed;
    
    public void SetUpGame(GameLevel level, Difficulty difficulty)
    {
        endSelectionPressed = false;
        Instantiate(difficulty.arena, Vector3.zero, Quaternion.identity);
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
        //if(GameManager.Instance != null) GameManager.Instance.musicManager.PlayMain();
        startAnimation.transform.parent.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        CurrentArena.StartGameRounds();
    }

    public void ClearField()
    {
        if(CurrentArena != null) Destroy(CurrentArena.gameObject);
        if(player != null) Destroy(player.transform.parent.gameObject);
        CurrentColors.Clear();
    }

    private bool IFrame = false;
    public void TakeDamage()
    {
        player.playerVisuals.DoHurt();
        damageSource.Play();
        playerHealth -= 1;
        HealthChangedListener?.Invoke(playerHealth);
        if(playerHealth <= 0) StopGame(false);
        //else StopCoroutine(IFrameTimer());
    }

    private IEnumerator IFrameTimer()
    {
        yield return new WaitForSeconds(.5f);
        IFrame = false;
    }

    public void StopGame(bool win)
    {
        Time.timeScale = 0;
        endAnimation.SetActive(true);
        endAnimation.GetComponent<DOTweenAnimation>().DOPlay();
        if (win)
        {
            endAnimation.transform.GetChild(1).gameObject.SetActive(true);
            winSource.Play();
            GameManager.Instance.musicManager.StopMusic();
        }
        else
        {
            endAnimation.transform.GetChild(0).gameObject.SetActive(true);
            GameManager.Instance.musicManager.PlayGameOver();
        }
        GameOverListener?.Invoke(win);
    }

    public void RestartGame()
    {
        if (!endSelectionPressed)
        {
            Time.timeScale = 1f;
            GameManager.Instance.LoadLevel(currentLevel);
            endSelectionPressed = true;
        }
    }
    
    public void GoBackToTitle()
    {
        if (!endSelectionPressed)
        {
            Time.timeScale = 1f;
            GameManager.Instance.LoadTitle();
            endSelectionPressed = true;
        }
    }
    
    public void GoBackToLevelSelect()
    {
        if (!endSelectionPressed)
        {
            Time.timeScale = 1f;
        GameManager.Instance.LoadLevelSelect();
            endSelectionPressed = true;
        }
    }
}
