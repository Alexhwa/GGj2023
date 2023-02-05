using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance ? _instance : _instance = FindObjectOfType<GameManager>();
    public MusicManager musicManager;

    public Difficulty selectedDifficulty;
    
    private void Awake()
    {
        if (_instance == null) _instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        musicManager.PlayTitle();
    }

    public void LoadLevel(GameLevel level)
    {
        StartCoroutine(LoadSequence("Game", level));
    }

    private IEnumerator LoadSequence(string scene, GameLevel level)
    {
        anim.Play("black-in");
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene(scene);
        while (SceneManager.GetActiveScene().name != scene) yield return null;
        anim.Play("black-out");
        musicManager.PlayMain();
        yield return new WaitForSeconds(.5f);
        GameController.Instance.SetUpGame(level, selectedDifficulty);
    }

    public void LoseLevel()
    {
        
    }

    public void WinLevel()
    {
        
    }

    public void PauseGame(bool pause)
    {
        
    }

    public void LoadTitle()
    {
        StartCoroutine(LoadSequence("Title"));
        musicManager.PlayTitle();
    }
    public void LoadLevelSelect()
    {
        StartCoroutine(LoadSequence("Title"));
        musicManager.PlayTitle();
    }

    private void LoadScene(string scene)
    {
        StartCoroutine(LoadSequence(scene));
    }

    [SerializeField] private Animator anim;
    private IEnumerator LoadSequence(string scene)
    {
        //anim.Play("black-in");
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene(scene);
        //while (SceneManager.GetActiveScene().name != scene) yield return null;
        anim.Play("black-out");
        yield return new WaitForSeconds(.5f);
    }
    
    public void SetDifficulty(Difficulty difficulty)
    {
        selectedDifficulty = difficulty;
    }
}
