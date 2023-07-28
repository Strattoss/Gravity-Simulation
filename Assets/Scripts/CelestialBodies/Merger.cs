using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merger : MonoBehaviour
{

    void OnCollisionEnter(Collision collision) {
        // Debug.Log("Collision from: " + gameObject.GetComponentInParent<Gravitybody>().gameObject.name + " with: " + collision.gameObject.GetComponentInParent<Gravitybody>().name);
        Gravitybody otherGb = collision.gameObject.GetComponentInParent<Gravitybody>();

        Merge(otherGb);
    }

    private void Merge(Gravitybody otherGb) {
        if (IsInferior(otherGb)) {
            // Debug.Log("I (" + gameObject.GetComponentInParent<Gravitybody>().gameObject.name + ") am inferior to " + otherGb.gameObject.name + ". Doing nothing");
            // the other Merger will take care of ths collision and destory me, just disabble myself
            gameObject.SetActive(false);
        }
        else {
            // I am superior
            // Debug.Log("I (" + gameObject.GetComponentInParent<Gravitybody>().gameObject.name + ") am superior to" + otherGb.gameObject.name + ". Initialising merging");
            GetComponentInParent<Gravitybody>().MergeWith(otherGb);
        }
    }

    // Used for comparisons. Decides eg. which body will be "eaten" by another during merging
    private bool IsInferior(Gravitybody otherGb)
    {
        Gravitybody _gb = GetComponentInParent<Gravitybody>();
        if (otherGb.mass > _gb.mass) return true;
        if (otherGb.mass < _gb.mass) return false;

        // equal velocity magnitudes are highly unlikely
        return otherGb.velocity.magnitude > _gb.velocity.magnitude;
    }

    // Used for comparisons. Decides eg. which body will be "eaten" by another during merging
    private bool IsSuperior(Gravitybody otherGb)
    {
        return !IsInferior(otherGb);
    }
}
