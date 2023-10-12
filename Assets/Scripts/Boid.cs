using UnityEngine;

public class Boid : MonoBehaviour
{
    private Vector3 velocity, acceleration;
    public BoidSettings settings;
    public int numCurrentFlock;
    public Vector3 position, forward;
    private Transform cachedTransform, target;
    public Vector3 avgFlockHeading, avgAvoidanceHeading, centreOfFlock;

    private MeshRenderer[] renderers;
    private TrailRenderer trail;

    private void Awake()
    {
        renderers = GetComponentsInChildren<MeshRenderer>();
        trail = GetComponent<TrailRenderer>();
        cachedTransform = transform;
    }

    public void Initialize(BoidSettings boidSettings, Transform targetTransform)
    {
        target = targetTransform;
        settings = boidSettings;
        position = cachedTransform.position;
        forward = cachedTransform.forward;
        velocity = transform.forward * (settings.minSpeed + settings.maxSpeed) * 0.5f;
    }

    public void SetColor(Color color)
    {
        foreach (var renderer in renderers)
            renderer.material.color = color;
        trail.startColor = color;
        trail.endColor = color;
    }

    public void UpdateBoid()
    {
        acceleration = Vector3.zero;

        if (target != null)
            acceleration += SteerTowards(target.position - position) * settings.targetWeight;

        if (numCurrentFlock != 0)
        {
            centreOfFlock /= numCurrentFlock;
            acceleration += SteerTowards(avgFlockHeading) * settings.alignWeight;
            acceleration += SteerTowards(centreOfFlock - position) * settings.cohesionWeight;
            acceleration += SteerTowards(avgAvoidanceHeading) * settings.separateWeight;
        }

        if (IsHeadingForCollision())
            acceleration += SteerTowards(ObstacleRays()) * settings.avoidCollisionWeight;

        velocity += acceleration * Time.deltaTime;
        velocity = Vector3.ClampMagnitude(velocity, settings.maxSpeed);
        cachedTransform.position += velocity * Time.deltaTime;
        cachedTransform.forward = (velocity / velocity.magnitude);
        position = cachedTransform.position;
        forward = cachedTransform.forward;
    }

    private bool IsHeadingForCollision()
    {
        return Physics.SphereCast(position, settings.boundsRadius, forward, out var hit, settings.collisionAvoidDistance, settings.obstacleMask);
    }

    private Vector3 ObstacleRays()
    {
        foreach (var dir in RaycastDirections.Directions)
        {
            var worldDir = cachedTransform.TransformDirection(dir);
            var ray = new Ray(position, worldDir);
            if (!Physics.SphereCast(ray, settings.boundsRadius, settings.collisionAvoidDistance, settings.obstacleMask))
                return worldDir;
        }
        return forward;
    }

    private Vector3 SteerTowards(Vector3 vector)
    {
        return Vector3.ClampMagnitude(vector.normalized * settings.maxSpeed - velocity, settings.maxSteerForce);
    }
}
