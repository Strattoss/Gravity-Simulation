using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityVectorScaler : VectorScaler
{
    void Start() {
        gb = GetComponentInParent<Gravitybody>();
    }

    public override void Update()
    {
        RotateToDirection(gb.velocity);
        ScaleLength(gb.velocity.magnitude);
    }
}
