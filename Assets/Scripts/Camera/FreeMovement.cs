using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeMovement : MonoBehaviour
{

    /*
    Made simple to use (drag and drop, done) for regular keyboard and mouse layouts

    Keyboard:
        wasd : Basic movement
        shift : Makes camera accelerate
        space : Moves camera on X and Z axes only.  So camera doesn't gain any height

    Mouse:
        right button held : Rotates the camera
        mouse scroll : Makes camera move forward. Works like zooming
        mouse scroll held : "Slides" camera along its relative XY plane. Won't move along Z axis, so it won't go "forward" nor "backward" 
    */


    [SerializeField] private float _mainSpeed = 40.0f; // regular camera speed
    [SerializeField] private float _shiftAdd = 120.0f; // multiplied by how long shift is held.  Basically running
    [SerializeField] private float _maxShift = 500.0f; // maximum speed when holdin shift
    [SerializeField] private float _slidingSens = 0.25f;
    [SerializeField] private float _rotationSens = 1.25f;
    [SerializeField] private float _zoomSens = 5f;
    [SerializeField] private float _totalRun = 1.0f; // How much additional speed the camera has accumulated

    void LateUpdate()
    {
        // Right mouse button rotation
        if (Input.GetButton("Fire2"))
        {
            Vector3 eulerRotation = new Vector3(-Input.GetAxis("Mouse Y") * _rotationSens, Input.GetAxis("Mouse X") * _rotationSens, 0);
            eulerRotation = new Vector3(transform.eulerAngles.x + eulerRotation.x, transform.eulerAngles.y + eulerRotation.y, 0);
            transform.eulerAngles = eulerRotation;
        }

        // Middle mouse button "sliding"
        if (Input.GetButton("Fire3"))
        {
            transform.Translate(-Input.GetAxis("Mouse X") * _slidingSens * Vector3.right);
            transform.Translate(-Input.GetAxis("Mouse Y") * _slidingSens * Vector3.up);
        }

        // Mouse wheel zooming
        transform.Translate(Vector3.forward * Input.GetAxis("Mouse ScrollWheel") * _zoomSens);


        // Keyboard commands
        Vector3 p = GetBaseInput();
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _totalRun += Time.deltaTime;
            p = p * _totalRun * _shiftAdd;
            p.x = Mathf.Clamp(p.x, -_maxShift, _maxShift);
            p.y = Mathf.Clamp(p.y, -_maxShift, _maxShift);
            p.z = Mathf.Clamp(p.z, -_maxShift, _maxShift);
        }
        else
        {
            _totalRun = Mathf.Clamp(_totalRun * 0.5f, 1f, 1000f);
            p = p * _mainSpeed;
        }

        p = p * Time.deltaTime;
        Vector3 newPosition = transform.position;
        if (Input.GetKey(KeyCode.Space))
        { // if player wants to move on X and Z axis only
            transform.Translate(p);
            newPosition.x = transform.position.x;
            newPosition.z = transform.position.z;
            transform.position = newPosition;
        }
        else
        {
            transform.Translate(p);
        }
    }

    private Vector3 GetBaseInput()
    { //returns the basic values, if it's 0 than it's not active.
        Vector3 p_Velocity = new Vector3();
        if (Input.GetKey(KeyCode.W))
        {
            p_Velocity += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            p_Velocity += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            p_Velocity += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            p_Velocity += new Vector3(1, 0, 0);
        }
        return p_Velocity;
    }
}
