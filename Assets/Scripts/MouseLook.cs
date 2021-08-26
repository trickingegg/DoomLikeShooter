using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public enum RotationAxes
    {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2
    }
    public RotationAxes axes = RotationAxes.MouseXAndY;

    public float sensivityHor = 9.0f;
    public float sensivityVert = 9.0f;

    public float minVert = -45.0f;
    public float maxVert = 45.0f;

    private float _rotationX = 0;
    void Start()
    {
        Rigidbody body = GetComponent<Rigidbody>();
        if (body != null)
            body.freezeRotation = true;
    }

    
    void Update()
    {
        if (axes == RotationAxes.MouseX)
        {
            //horizontal spin
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensivityHor, 0);
        }
        else if (axes == RotationAxes.MouseY)
        {
            //vertical spin
            _rotationX -= Input.GetAxis("Mouse Y") * sensivityVert; //increase vertical rotation angle accordingly to mouse movement
            _rotationX = Mathf.Clamp(_rotationX, minVert, maxVert); // fixating the angle with the range of min & max

            float rotationY = transform.localEulerAngles.y; // save angle without horiz rotation

            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0); // create new vector from saved rotation data
        }
        else
        {
            //combined
            _rotationX -= Input.GetAxis("Mouse Y") * sensivityVert;
            _rotationX = Mathf.Clamp(_rotationX, minVert, maxVert);

            float delta = Input.GetAxis("Mouse X") * sensivityHor; //amount of change in rotation angle
            float rotationY = transform.localEulerAngles.y + delta; //increment the angle with delta

            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        }
    }
}
