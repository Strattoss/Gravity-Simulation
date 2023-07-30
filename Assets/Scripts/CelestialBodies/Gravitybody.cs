using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravitybody : MonoBehaviour
{
    private SimulationManager _simulationManager;
    public Vector3 position
    {
        get { return transform.position; }
        private set { transform.position = value; }
    }

    [field: SerializeField] public Vector3 velocity { get; private set; }

    public Vector3 momentum
    {
        get
        {
            return velocity * mass;
        }
    }

    [field: SerializeField] public Vector3 force { get; private set; }

    public float kineticEnergy
    {
        get
        {
            return 0.5f * mass * velocity.sqrMagnitude;
        }
    }

    [field:SerializeField] public float mass { get; private set;}

    // [SerializeField] private bool interacts = true;

    public void SearchForAndSetGravityManager() {
        GameObject[] simulationManagers = GameObject.FindGameObjectsWithTag("SimulationManager");
        if (simulationManagers.Length != 1) {
            throw new System.Exception("There should be exactly one SimulationManager!");
        }
        _simulationManager = simulationManagers[0].GetComponent<SimulationManager>();
    }

    protected void SignOutOfGravityManager()
    {
        // Debug.Log("Signing out: " + this);
        _simulationManager.SignGravitybodyOut(this);
    }

    public void MergeWith(Gravitybody otherGb) {
        otherGb.SignOutOfGravityManager();

        // average position regarding positions with masses as weights
        position = (position * mass + otherGb.position * otherGb.mass) / (mass + otherGb.mass);

        velocity = (momentum + otherGb.momentum) / (mass + otherGb.mass);

        mass += otherGb.mass;

        GetComponent<AudioSource>().Play();
        Destroy(otherGb.gameObject);
    }

    #region Verlet Integration
    /*
    This paper helped a looot: https://young.physics.ucsc.edu/115/leapfrog.pdf
    Shortened algorithm idea:
     - Vector3 halfPosition = earthRb.position + 0.5f * dt * velocity;
     - velocity = velocity + dt * CalculateForce(halfPosition);
     - earthRb.position = halfPosition + 0.5f * dt * velocity;
    */

    /// <summary> First step of Verlet velocity method. It updates body's position by half the real amount </summary>
    public void VerletStep1()
    {
        position += 0.5f * Time.fixedDeltaTime * velocity;
    }

    /// <summary> Second step of Verlet velocity method. Updates body's velocity </summary>
    public void VerletStep2()
    {
        force = CalculateForce();
        velocity += Time.fixedDeltaTime * force / mass;
    }

    /// <summary> Third and last step of Verlet velocity method. Updates body's final position </summary>
    public void VerletStep3()
    {
        position += 0.5f * Time.fixedDeltaTime * velocity;
    }


    Vector3 CalculateForce()
    {
        Vector3 summedForce = new Vector3(0, 0, 0);
        Vector3 distance;

        foreach (var otherGravitybody in _simulationManager.gravitybodies)
        {
            if (otherGravitybody == this) continue;
            distance = position - otherGravitybody.position;
            summedForce += (-1) * PhysicalConstants.gravitationalConstant * otherGravitybody.mass * mass * distance.normalized / distance.sqrMagnitude;

        }

        return summedForce;
    }
    #endregion

    
}
