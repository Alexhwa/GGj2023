using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private List<GameObject> healthNuggets;
    
    private void Start()
    {
        GameController.Instance.HealthChangedListener += UpdateHealth;   
    }

    private void UpdateHealth(int health)
    {
        foreach (var h in healthNuggets)
        {
            h.SetActive(health > 0);
            health -= 1;
        }
    }

    private void OnDestroy()
    {
        if(GameController.Instance) GameController.Instance.HealthChangedListener -= UpdateHealth;
    }
}
