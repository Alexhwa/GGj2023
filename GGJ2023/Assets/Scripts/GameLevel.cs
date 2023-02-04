using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable][CreateAssetMenu]
public class GameLevel : ScriptableObject
{
    [Serializable]
    public struct Wall
    {
        public GameColor.COLOR color;
        public int startingArms;
    }

    [Tooltip("First wall is Top / Top-Left, increases clockwise")]
    public List<Wall> walls;

    [Serializable]
    public struct Round
    {
        public int enemies;
        public int time;
    }

    public List<Round> rounds;

}
