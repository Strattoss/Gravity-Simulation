using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravitybody : MonoBehaviour
{
    public GravityManager simulationManager;
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

    public float kineticEnergy
    {
        get
        {
            return 0.5f * mass * velocity.sqrMagnitude;
        }
    }

    // _mass property exists only so I can see it and modify it in the Inspector
    [SerializeField] private float _mass = 1.0f;
    public float mass
    {
        get { return _mass; }
        set
        {
            // Update the Rigidbody's mass when the property is set
            _mass = value;
            GetComponent<Rigidbody>().mass = _mass;
        }
    }
    // old implementation
    // public float mass
    // {
    //     get
    //     {
    //         return GetComponent<Rigidbody>().mass;
    //     }
    //     set
    //     {
    //         GetComponent<Rigidbody>().mass = value;
    //     }
    // }

    // [SerializeField] private bool interacts = true;

    // private void SignOutOfGravityManager()
    // {
    //     simulationManager.SignGravitybodyOut(this);
    // }

    #region Prediction Line Manipulation
    private void RemoveLastPointFromPredictionLine()
    {
        LineRenderer lineRenderer = this.GetComponent<LineRenderer>();

        if (lineRenderer.positionCount > 0)
        {
            lineRenderer.positionCount--;
        }
    }

    public void TrimPredictionLine()
    {
        // LineRenderer lineRenderer = this.GetComponent<LineRenderer>();
        // for (int i = 0; i < lineRenderer.positionCount; i++)
        // {
        //     if (lineRenderer.GetPosition(i).Equals(Vector3.zero)) {
        //         Vector3[] positions = new Vector3[lineRenderer.positionCount];
        //         lineRenderer.GetPositions(positions);

        //         lineRenderer.positionCount = i;
        //         Vector3[] newPositions = new Vector3[lineRenderer.positionCount - i];
        //         for (int j = 0; j < lineRenderer.positionCount - i; j++)
        //         {
        //             newPositions[j] = positions[i+j];
        //         }

        //         lineRenderer.SetPositions(newPositions);
        //         break;
        //     }
        // }
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        int shift = 0;

        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            if (!lineRenderer.GetPosition(i).Equals(Vector3.zero))
            {
                // Shift non-zero positions to the front of the array
                // TODO: Czy to wykonuje się tylko raz?
                lineRenderer.SetPosition(i - shift, lineRenderer.GetPosition(i));
            }
            else
            {
                shift++;
            }
        }

        // Set the new position count to remove the trailing zero positions
        lineRenderer.positionCount -= shift;
    }
    #endregion


    #region Verlet Integration
    /*  
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
        velocity += Time.fixedDeltaTime * CalculateForce() / mass;
    }

    /// <summary> Third and last step of Verlet velocity method. Updates body's final position </summary>
    public void VerletStep3()
    {
        position += 0.5f * Time.fixedDeltaTime * velocity;
        RemoveLastPointFromPredictionLine();
    }


    Vector3 CalculateForce()
    {
        Vector3 summedForce = new Vector3(0, 0, 0);
        Vector3 distance;

        foreach (var otherGravitybody in simulationManager.gravitybodies)
        {
            if (otherGravitybody == this) continue;
            distance = position - otherGravitybody.position;
            summedForce += (-1) * PhysicalConstants.gravitationalConstant * otherGravitybody.mass * mass * distance.normalized / distance.sqrMagnitude;

        }

        return summedForce;
    }
    #endregion

    // Used for comparisons. Decides eg. which body will be "eaten" by another during merging
    private bool IsInferior(Gravitybody otherGb)
    {
        if (otherGb.mass > mass) return true;
        if (otherGb.mass < mass) return false;

        // equal masses
        return otherGb.velocity.magnitude > velocity.magnitude;
    }

    // Used for comparisons. Decides eg. which body will be "eaten" by another during merging
    private bool IsSuperior(Gravitybody otherGb)
    {
        return !IsInferior(otherGb);
    }
    public override string ToString()
    {
        return "Name: " + transform.name + ", position: " + position + ", velocity: " + velocity + ", mass: " + mass;
    }
}
