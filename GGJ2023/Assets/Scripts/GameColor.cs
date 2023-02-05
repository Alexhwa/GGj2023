using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class GameColor
{
    public enum COLOR
    {
        None = -1,
        Red = 0,
        Yellow = 1,
        Green = 2,
        Blue = 3,
        Pink = 4,
        Purple = 5
        
    }

    private static Dictionary<COLOR, Color> ColorMap = new Dictionary<COLOR, Color>()
    {
        {COLOR.None, Color.white},{COLOR.Red, Color.red}, {COLOR.Blue, Color.blue}, {COLOR.Green, Color.green}, {COLOR.Yellow, Color.yellow},
        {COLOR.Pink, new Color(237/255f, 76/255f, 197/255f)}, {COLOR.Purple, new Color(140/255f, 52/255f, 235/255f)}
    };

    public static COLOR RandomColorExcluding(COLOR color)
    {
        List<COLOR> colors = new List<COLOR>(GameController.Instance.CurrentColors);
        colors.Remove(color);
        return colors[Random.Range(0, colors.Count)];
    }

    public static Color GetColor(COLOR color)
    {
        return ColorMap[color];
    }
    
}