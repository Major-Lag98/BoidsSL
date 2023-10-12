using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public Boid prefab;
    public float spawnRadius = 10;
    public int spawnCount = 10;

    private void Start()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 randomPosition = transform.position + Random.insideUnitSphere * spawnRadius;
            Boid boid = Instantiate(prefab, randomPosition, Quaternion.identity);
            boid.transform.forward = Random.insideUnitSphere;
            boid.SetColor(Random.ColorHSV());
        }
    }
}
