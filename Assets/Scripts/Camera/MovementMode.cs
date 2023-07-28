using UnityEngine;

public class MovementMode : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button click
        {
            // Cast a ray from the mouse position into the scene
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the ray hits any colliders in the scene
            if (Physics.Raycast(ray, out hit))
            {
                // The ray has hit something
                // You can now handle the hit object or perform any desired actions
                SelectFollowSmoothly(hit.collider.transform);
            }
            else {
                SelectFreeMovement();
            }
        }
    }

    public void SelectFreeMovement() {
        GetComponent<FollowSmoothly>().enabled = false;
        GetComponent<FreeMovement>().enabled = true;
    }

    public void SelectFollowSmoothly(Transform transform) {
        GetComponent<FreeMovement>().enabled = false;
        GetComponent<FollowSmoothly>().target = transform;
        GetComponent<FollowSmoothly>().enabled = true;
    }
}