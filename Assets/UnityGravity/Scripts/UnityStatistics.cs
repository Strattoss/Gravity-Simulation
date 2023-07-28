using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityStatistics : MonoBehaviour
{
    private readonly List<Rigidbody> rbs = new List<Rigidbody>();

    public float kineticEnergy, potentialEnergy, totalEnergy;
    public float minTotalEnergy = 1000, maxTotalenergy = -1000;
    public Vector3 momentum;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("GravityObject");
        foreach (GameObject gameObject in gameObjects)
        {
            rbs.Add(gameObject.GetComponent<Rigidbody>());
        }

        foreach (Rigidbody rb in rbs)
        {
            momentum += rb.velocity * rb.mass;
        }
    }

    // Update is called once per frame
    void Update()
    {
        kineticEnergy = potentialEnergy = totalEnergy = 0;

        foreach (Rigidbody rb in rbs)
        {
            kineticEnergy += rb.mass * rb.velocity.sqrMagnitude / 2;
        }

        for (int i = 0; i < rbs.Count; i++)
        {
            for (int j = i+1; j < rbs.Count; j++)
            {
                float distance = (rbs[i].position - rbs[j].position).magnitude;
                potentialEnergy += - PhysicalConstants.gravitationalConstant * rbs[i].mass * rbs[j].mass / distance;
            }
        }

        totalEnergy = kineticEnergy + potentialEnergy;

        minTotalEnergy = Mathf.Min(minTotalEnergy, totalEnergy);
        maxTotalenergy = Mathf.Max(maxTotalenergy, totalEnergy);

        momentum = new Vector3(0, 0, 0);
        foreach (Rigidbody rb in rbs)
        {
            momentum += rb.velocity * rb.mass;
        }
    }
}
