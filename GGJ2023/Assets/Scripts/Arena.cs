using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena : MonoBehaviour
{
    [SerializeField] private List<Wall> walls;

    public void InitArena(GameLevel level)
    {
        for(int i = 0; i < walls.Count; i++)
        {
            walls[i].InitWall(level.walls[i].color, level.walls[i].startingArms);
            if (level.walls[i].color != GameColor.COLOR.None)
                GameController.Instance.CurrentColors.Add(level.walls[i].color);
        }
    }

    public void ChangeWallPlacement(bool enabled)
    {
        foreach (var w in walls)
        {
            w.clickArea.enabled = enabled;
        }
    }
}
