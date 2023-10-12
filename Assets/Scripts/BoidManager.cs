using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidManager : MonoBehaviour
{
    public ComputeShader compute;
    public BoidSettings settings;
    private Boid[] boids;

    private void Start()
    {
        boids = FindObjectsOfType<Boid>();
        foreach (Boid boid in boids)
        {
            boid.Initialize(settings, null);
        }
    }

    private void Update()
    {
        if (boids == null) return;

        int numBoids = boids.Length;
        var boidData = new BoidData[numBoids];

        for (int i = 0; i < numBoids; i++)
        {
            boidData[i].position = boids[i].position;
            boidData[i].direction = boids[i].forward;
        }

        ComputeBuffer boidBuffer = new ComputeBuffer(numBoids, BoidData.Size);
        boidBuffer.SetData(boidData);

        compute.SetBuffer(0, "boids", boidBuffer);
        compute.SetInt("numBoids", numBoids);
        compute.SetFloat("viewRadius", settings.perceptionRadius);
        compute.SetFloat("avoidRadius", settings.avoidanceRadius);

        int threadGroups = Mathf.CeilToInt(numBoids / 1024f);
        compute.Dispatch(0, threadGroups, 1, 1);

        boidBuffer.GetData(boidData);

        for (int i = 0; i < numBoids; i++)
        {
            Boid boid = boids[i];
            boid.avgFlockHeading = boidData[i].flockHeading;
            boid.centreOfFlock = boidData[i].flockCentre;
            boid.avgAvoidanceHeading = boidData[i].avoidanceHeading;
            boid.numCurrentFlock = boidData[i].numInFlock;
            boid.UpdateBoid();
        }

        boidBuffer.Release();
    }

    public struct BoidData
    {
        public Vector3 position;
        public Vector3 direction;
        public Vector3 flockHeading;
        public Vector3 flockCentre;
        public Vector3 avoidanceHeading;
        public int numInFlock;

        public static int Size
        {
            get { return sizeof(float) * 3 * 5 + sizeof(int); }
        }
    }
}
