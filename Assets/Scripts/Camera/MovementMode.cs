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
                GetComponent<FreeMovement>().enabled = false;
                GetComponent<FollowSmoothly>().target = hit.collider.transform;
                GetComponent<FollowSmoothly>().enabled = true;
            }
            else {
                GetComponent<FollowSmoothly>().enabled = false;
                GetComponent<FreeMovement>().enabled = true;
            }
        }
    }
}