using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
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
    
}