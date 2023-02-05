using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{

    public UnityEvent<int> onSpawn = new UnityEvent<int>();
    public UnityEvent onFinishedSpawn = new UnityEvent();
    public Difficulty currentDiffculty;

    private float elapsedTime;
    private float spawnTimer;
    private bool isSpawning;

    private SpawnPattern currentPattern;
    private int currentPatternIndex;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isSpawning)
        {
            elapsedTime += Time.deltaTime;
            spawnTimer -= Time.deltaTime;
            if(spawnTimer <= 0)
            {
                Spawn();
                spawnTimer = GetSpawnInterval();
            }
            if (elapsedTime >= currentDiffculty.duration)
            {
                onFinishedSpawn.Invoke();
                Reset();
            }
        }
    }

    public void StartSpawning()
    {
        isSpawning = true;
        spawnTimer = GetSpawnInterval();
        currentPattern = currentDiffculty.patterns[Random.Range(0, currentDiffculty.patterns.Count)];
    }

    private void Reset()
    {
        elapsedTime = 0;
        spawnTimer = 0;
        isSpawning = false;
        currentPattern = null;
    }

    private float GetSpawnInterval()
    {
        var intervalWithDeviation = currentDiffculty.spawnInterval + Random.Range(0, currentDiffculty.spawnIntervalDeviation);
        return intervalWithDeviation / (1 + currentDiffculty.speedUpScale * elapsedTime / currentDiffculty.duration);
    }

    private void Spawn()
    {
        var usedPattern = currentPattern;
        onSpawn.Invoke(currentPattern.spawnObjects[currentPatternIndex]);
        currentPatternIndex++;
        if (currentPatternIndex == currentPattern.spawnObjects.Count)
        {
            while(currentPattern == usedPattern)
                currentPattern = currentDiffculty.patterns[Random.Range(0, currentDiffculty.patterns.Count)];
            currentPatternIndex = 0;
        }
    }

    public string GetTimerTime()
    {
        return ((int)(currentDiffculty.duration - elapsedTime)).ToString();
    }
}
