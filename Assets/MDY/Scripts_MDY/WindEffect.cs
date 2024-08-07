using UnityEngine;

public class WindEffect : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 currentWindDirection;
    private float currentWindStrength;
    private bool isInWindZone1 = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (isInWindZone1)
        {
            ApplyWindForce();
        }
    }

    public void ApplyWind(Vector3 direction, float strength)
    {
        currentWindDirection = direction;
        currentWindStrength = strength;
        isInWindZone1 = true;
    }

    public void RemoveWind()
    {
        isInWindZone1 = false;
    }

    private void ApplyWindForce()
    {
        if (rb != null)
        {
            rb.AddForce(currentWindDirection.normalized * currentWindStrength);
        }
    }
}