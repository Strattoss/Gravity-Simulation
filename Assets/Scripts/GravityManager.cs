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

    void FixedUpdate()
    {
        DoScenewideVerletStep();
    }

    public void DoScenewideVerletStep()
    {
        foreach (var gravitybody in gravitybodies)
        {
            gravitybody.VerletStep1();
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
