using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MovementDirections
{
    public const int NumViewDirections = 300;
    public static readonly Vector3[] Directions;

    static MovementDirections()
    {
        Directions = GenerateDirections(NumViewDirections);
    }

    private static Vector3[] GenerateDirections(int numDirections)
    {
        var directions = new Vector3[numDirections];
        float goldenRatio = (1 + Mathf.Sqrt(5)) / 2;
        float angleIncrement = 2 * Mathf.PI * goldenRatio;

        for (int i = 0; i < numDirections; i++)
        {
            float step = i / (float)numDirections;
            float inclination = Mathf.Acos(1 - 2 * step);
            float azimuth = angleIncrement * i;

            float x = Mathf.Sin(inclination) * Mathf.Cos(azimuth);
            float y = Mathf.Sin(inclination) * Mathf.Sin(azimuth);
            float z = Mathf.Cos(inclination);
            directions[i] = new Vector3(x, y, z);
        }

        return directions;
    }
}
