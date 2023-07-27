using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityGravitybody : MonoBehaviour
{
    public Vector3 initialVelocity;
    public Vector3 currentVelocity;
    public List<GameObject> gravityGameObjects;
    private readonly List<Rigidbody> gravityRigidbodies = new List<Rigidbody>();

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        if (gravityGameObjects.Count == 0) {
            gravityGameObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("GravityObject"));
        }
        
        // create a list of rigidbodies so we don't have to access them over and over again
        foreach (GameObject gravityPartner in gravityGameObjects)
        {
            gravityRigidbodies.Add(gravityPartner.GetComponent<Rigidbody>());
        }
        // object cannot interact gravitationally with itself
        gravityRigidbodies.Remove(GetComponent<Rigidbody>());

        rb = GetComponent<Rigidbody>();
        rb.velocity = initialVelocity;
    }

    void FixedUpdate()
    {
        foreach (Rigidbody otherRb in gravityRigidbodies)
        {
            Vector3 distance = (otherRb.position - rb.position);
            rb.AddForce(rb.mass * otherRb.mass  * PhysicalConstants.gravitationalConstant * distance / (Mathf.Pow(distance.magnitude, 3)));
        }
        
    }
}
