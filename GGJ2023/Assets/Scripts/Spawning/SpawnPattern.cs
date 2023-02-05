using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]

[CreateAssetMenu(fileName = "New Spawn Pattern", menuName = "Spawning/Spawn Pattern")]
public class SpawnPattern : ScriptableObject
{
    [Tooltip("Put the number for how many guys should spawn. -1 spawns a charger")]
    public List<int> spawnObjects;
}