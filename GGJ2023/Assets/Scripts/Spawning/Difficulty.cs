using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Difficulty", menuName = "Spawning/Difficulty")]
public class Difficulty : ScriptableObject
{
    public float duration;
    public float spawnInterval;
    [Tooltip("Max amount of time randomly added to spawn intervals")]
    public float spawnIntervalDeviation;
    [Tooltip("The multiplier applied to the time scale of spawning as the round timer continues. At the end of the round, the spawn interals should be spawnInterval / speedUpScale")]
    public float speedUpScale;
    public List<SpawnPattern> patterns;
    public GameObject arena;
}
