using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class CelestialBodyStyler : MonoBehaviour
{
    protected float hueMin = 0, hueMax = 1, saturationMin = 0.75f, saturationMax = 1f, valueMin = 0.75f, valueMax = 1f;
    [SerializeField] protected Color myColor;

    void Start()
    {
        RollMyNewColor();
        ApplyMyNewColor();
    }

    protected virtual void RollMyNewColor() {
        myColor = Random.ColorHSV(hueMin, hueMax, saturationMin, saturationMax, valueMin, valueMax);
    }

    protected virtual void ApplyMyNewColor()
    {
        GetComponent<MeshRenderer>().material.color = myColor;
        GetComponent<TrailRenderer>().startColor = myColor;
        GetComponent<LineRenderer>().material.color = myColor;
    }
}
