using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceVectorScaler : VectorScaler
{
    void Start() {
        gb = GetComponentInParent<Gravitybody>();
    }

    public override void Update()
    {
        RotateToDirection(gb.force);
        ScaleLength(gb.force.magnitude);
    }
}
