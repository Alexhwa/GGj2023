using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance ? _instance : _instance = FindObjectOfType<GameManager>();
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void LoadLevel(GameLevel level)
    {
        StartCoroutine(LoadSequence("Game", level));
    }

    private IEnumerator LoadSequence(string scene, GameLevel level)
    {
        SceneManager.LoadScene(scene);
        while (SceneManager.GetActiveScene().name != scene) yield return null;
        GameController.Instance.SetUpGame(level);
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
        
    }
    public void LoadLevelSelect()
    {
        
    }
    
}
