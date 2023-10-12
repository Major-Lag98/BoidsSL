using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RaycastDirections
{
    public const int NumViewDirections = 300;
    public static readonly Vector3[] Directions;

    static RaycastDirections()
    {
        Directions = GenerateDirections(NumViewDirections);
    }

    private static Vector3[] GenerateDirections(int numDirections)
    {
        var directions = new Vector3[numDirections];
        float goldenRatio = (1 + Mathf.Sqrt(5)) / 2;
        float angleIncrement = Mathf.PI * 2 * goldenRatio;

        for (int i = 0; i < numDirections; i++)
        {
            float t = i / (float)numDirections;
            float inclination = Mathf.Acos(1 - 2 * t);
            float azimuth = angleIncrement * i;

            float x = Mathf.Sin(inclination) * Mathf.Cos(azimuth);
            float y = Mathf.Sin(inclination) * Mathf.Sin(azimuth);
            float z = Mathf.Cos(inclination);
            directions[i] = new Vector3(x, y, z);
        }

        return directions;
    }
}
