using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManager : MonoBehaviour
{
    public LinkedList<Gravitybody> gravitybodies { get; } = new LinkedList<Gravitybody>();

    private GameObject _celestialBodiesParent;
    public GameObject celestialBodiesParent
    {
        get {
            return _celestialBodiesParent;
        }
        set
        {
            _celestialBodiesParent = value;
            ReloadGravitybodies();
        }
    }

    void Start()
    {
        celestialBodiesParent = GameObject.FindGameObjectWithTag("CelestialBodiesParent");
    }

    private void ReloadGravitybodies()
    {
        gravitybodies.Clear();
        foreach (Gravitybody gb in _celestialBodiesParent.GetComponentsInChildren<Gravitybody>())
        {
            gravitybodies.AddFirst(gb);
            gb.SearchForAndSetGravityManager();
        }
    }

    public void SignGravitybodyOut(Gravitybody gb) {
        // Debug.Log("Before: " + gravitybodies.Count);
        var b = gravitybodies.Remove(gb);
        // Debug.Log("Result of signing out: " + b);
        // Debug.Log("After: " + gravitybodies.Count);
    }

    void FixedUpdate()
    {
        DoScenewideVerletStep();
    }

    public void DoScenewideVerletStep()
    {
        int i = 0;
        foreach (var gravitybody in gravitybodies)
        {
            // Debug.Log("Trying Verlet1 on: " + gravitybody.name + " on position: " + i);
            gravitybody.VerletStep1();
            i++;
        }

        foreach (var gravitybody in gravitybodies)
        {
            gravitybody.VerletStep2();
        }

        foreach (var gravitybody in gravitybodies)
        {
            gravitybody.VerletStep3();
        }
    }
}
