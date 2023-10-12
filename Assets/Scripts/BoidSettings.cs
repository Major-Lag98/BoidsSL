using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidSettings : ScriptableObject {
    public float minSpeed = 1;
    public float maxSpeed = 7;
    public float perceptionRadius = 5;
    public float avoidanceRadius = 1;
    public float maxSteerForce = 5;

    public float alignWeight = 1;
    public float cohesionWeight = 1;
    public float separateWeight = 1;
    public float targetWeight = 1;

    public LayerMask obstacleMask;
    public float boundsRadius = .20f;
    public float avoidCollisionWeight = 11;
    public float collisionAvoidDistance = 4;

}