using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarStyler : CelestialBodyStyler
{
    protected override void RollMyNewColor()
    {
        myColor = Random.ColorHSV(0, 65f/360, saturationMin, saturationMax, 1, 1);
    }

    protected override void ApplyMyNewColor() {
        base.ApplyMyNewColor();
        GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", myColor);
        GetComponentInChildren<Light>().color = myColor;
    }
}
