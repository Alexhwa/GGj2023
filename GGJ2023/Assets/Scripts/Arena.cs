using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Arena : MonoBehaviour
{
    [SerializeField] private GameObject enemyObject;
    [SerializeField] private List<Wall> walls;

    public void InitArena(GameLevel level)
    {
        for(int i = 0; i < walls.Count; i++)
        {
            walls[i].InitWall(level.walls[i].color, level.walls[i].startingArms);
            if (level.walls[i].color != GameColor.COLOR.None)
                GameController.Instance.CurrentColors.Add(level.walls[i].color);
        }

        StartCoroutine(HandleGameRounds(level.rounds));
    }
    
    private IEnumerator HandleGameRounds(List<GameLevel.Round> rounds)
    {
        foreach (var round in rounds)
        {
            float increment = round.time / round.enemies;
            for (int i = 0; i < round.enemies; i++)
            {
                Instantiate(enemyObject);
                yield return new WaitForSeconds(increment);
            }
            //If you see this Alex, I'm sorry
            while (FindObjectOfType<Enemy>() != null) yield return new WaitForSeconds(.5f);
            //TODO: End Round;
        }
        //TODO: End Game
    }
}
