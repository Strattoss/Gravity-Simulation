using UnityEngine;

public class FollowSmoothly : MonoBehaviour
{
    public Transform target;
    [SerializeField] private float _distance = 10f;
    [SerializeField] public float _rotationSens = 5f;
    [SerializeField] private float _zoomSens = 10f;

    private Vector3 _offset;

    void Start()
    {
        _offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        if (target == null) {
            GetComponent<MovementMode>().SelectFreeMovement();
        }

        _distance -= Input.GetAxis("Mouse ScrollWheel") * _zoomSens;

        if (Input.GetButton("Fire2"))
        {
            float horizontalInput = Input.GetAxis("Mouse X") * _rotationSens;
            float verticalInput = Input.GetAxis("Mouse Y") * _rotationSens;

            // Rotate the camera around the target based on mouse input
            Vector3 eulerRotation = new Vector3(verticalInput, horizontalInput, 0f);
            Quaternion rotation = Quaternion.Euler(eulerRotation);
            _offset = rotation * _offset;
        }

        // Calculate the new camera position based on the target's position and distance
        Vector3 newPosition = target.position + _offset.normalized * _distance;

        // Smoothly move the camera to the new position
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * _zoomSens);

        // Make the camera always look at the target
        transform.LookAt(target);
    }


}