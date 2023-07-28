using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorScaler : MonoBehaviour
{
    protected Gravitybody gb;
    public float lengthMultiplier = 1f;

    void Start() {
        gb = GetComponentInParent<Gravitybody>();
    }

    public virtual void Update()
    {
        RotateToDirection(gb.velocity);
        ScaleLength(gb.velocity.magnitude);
    }

    protected void ScaleLength(float magnitude)
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, magnitude * lengthMultiplier);
    }

    protected void RotateToDirection(Vector3 direction)
    {
        transform.LookAt(transform.position + direction);
    }
}
