using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public void SetDifficulty(Difficulty d)
    {
        GameManager.Instance.SetDifficulty(d);
    }

    public void SetLevel(GameLevel level)
    {
        GameManager.Instance.LoadLevel(level);
    }
    
}
