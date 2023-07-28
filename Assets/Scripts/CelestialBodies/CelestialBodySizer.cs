using UnityEngine;

public class CelestialBodySizer : MonoBehaviour
{
    // public float mass
    // {
    //     get
    //     {
    //         return GetComponent<Gravitybody>().mass;
    //     }
    //     set
    //     {
    //         Debug.Log("Old mass: " + mass);
    //         Debug.Log("New mass: " + value);
    //         radius *= Mathf.Pow(value/mass, 1f/3);
    //         GetComponent<Gravitybody>().mass = value;
    //         Debug.Log("Density: " + density);
    //     }
    // }
    // public float radius
    // {
    //     get
    //     {
    //         return transform.localScale.x;
    //     }
    //     set
    //     {
    //         transform.localScale = new Vector3(value, value, value);
    //     }
    // }
    // public float density
    // {
    //     get
    //     {
    //         return 3 * mass / (4 * Mathf.PI * Mathf.Pow(radius, 3));
    //     }
    //     set
    //     {
    //         print(0.75f * Mathf.PI * mass / value);
    //         print(Mathf.Pow(0.75f * Mathf.PI * mass / value, 1f/3));
    //         radius = Mathf.Pow(0.75f  * mass / (Mathf.PI * value), 1f/3);
    //     }
    // }

    // void Update() {
    //     if (Input.GetKeyDown(KeyCode.K)) {
    //         mass += 1;
    //     }
    // }

}
