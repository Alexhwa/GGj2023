using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Color = System.Drawing.Color;


public static class GameColor
{
    public enum COLOR
    {
        None = -1,
        Red = 0,
        Blue = 1,
        Green = 2,
        Yellow = 3
    }

    private static Dictionary<COLOR, Color> ColorMap = new Dictionary<COLOR, Color>()
    {
        {COLOR.Red, Color.Red}, {COLOR.Blue, Color.Blue}, {COLOR.Green, Color.Green}, {COLOR.Yellow, Color.Yellow}
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