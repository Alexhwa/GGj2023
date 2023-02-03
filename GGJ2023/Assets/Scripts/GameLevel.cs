using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameLevel : ScriptableObject
{
    public struct Wall
    {
        public GameColor.COLOR color;
        public int startingArms;
    }

    [Tooltip("First wall is Top / Top-Left, increases clockwise")]
    public List<Wall> walls;
    
    //TODO: Incorporate enemy spawn frequency
}
