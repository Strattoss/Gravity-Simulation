using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statistics : MonoBehaviour
{
    private SimulationManager _gravityManager;
    [field:SerializeField] public float kineticEnergy { get; private set; }
    [field:SerializeField] public float potentialEnergy { get; private set; }
    [field:SerializeField] public float totalEnergy { get; private set; }
    public float minTotalEnergy = 1000, maxTotalenergy = -1000;
    public Vector3 momentum;


    void Start()
    {
        _gravityManager = GameObject.Find("SimulationManager").GetComponent<SimulationManager>();
    }

    void Update()
    {
        kineticEnergy = potentialEnergy = totalEnergy = 0;

        foreach (Gravitybody gb in _gravityManager.gravitybodies)
        {
            kineticEnergy += gb.kineticEnergy;
        }

        var nodeA = _gravityManager.gravitybodies.First;
        while (nodeA != null)
        {
            var nodeB = nodeA.Next;
            while (nodeB != null)
            {
                Vector3 distance = nodeA.Value.position - nodeB.Value.position;
                potentialEnergy += (-1) * PhysicalConstants.gravitationalConstant * nodeA.Value.mass * nodeB.Value.mass / distance.magnitude;

                nodeB = nodeB.Next;
            }

            nodeA = nodeA.Next;
        }

        // for (int i = 0; i < _gravityManager.gravitybodies.Count; i++)
        // {
        //     for (int j = i+1; j < _gravityManager.gravitybodies.Count; j++)
        //     {
        //         Vector3 distance = _gravityManager.gravitybodies[i].position - _gravityManager.gravitybodies[j].position;
        //         potentialEnergy += (-1) * PhysicalConstants.gravitationalConstant * _gravityManager.gravitybodies[i].mass * _gravityManager.gravitybodies[j].mass / distance.magnitude;
        //     }
        // }

        totalEnergy = kineticEnergy + potentialEnergy;

        minTotalEnergy = Mathf.Min(minTotalEnergy, totalEnergy);
        maxTotalenergy = Mathf.Max(maxTotalenergy, totalEnergy);

        momentum = new Vector3(0, 0, 0);
        foreach (Gravitybody gb in _gravityManager.gravitybodies)
        {
            momentum += gb.momentum;
        }
    }
}
