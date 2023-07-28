using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MomentumVectorScaler : VectorScaler
{
    void Start() {
        gb = GetComponentInParent<Gravitybody>();
    }

    public override void Update()
    {
        RotateToDirection(gb.momentum);
        ScaleLength(gb.momentum.magnitude);
    }
}
