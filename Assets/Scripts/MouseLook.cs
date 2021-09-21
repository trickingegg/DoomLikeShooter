using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RotationAxes
{
    MouseXAndY = 0,
    MouseX = 1,
    MouseY = 2
}

[RequireComponent(typeof(Rigidbody))]
public class MouseLook : MonoBehaviour
{
    public RotationAxes _axes = RotationAxes.MouseXAndY;

    public float SensivityHor = 9.0f;
    public float SensivityVert = 9.0f;
    public float MinVert = -45.0f;
    public float MaxVert = 45.0f;

    private float RotationX = 0;
    void Start()
    {
        Rigidbody body = GetComponent<Rigidbody>();
        if (body != null)
            body.freezeRotation = true;
    }
    
    void Update()
    {
        if (_axes == RotationAxes.MouseX)
            transform.Rotate(0, Input.GetAxis("Mouse X") * SensivityHor, 0); //horizontal spin
        else if (_axes == RotationAxes.MouseY)
        {
            //vertical spin
            RotationX -= Input.GetAxis("Mouse Y") * SensivityVert; //increase vertical rotation angle accordingly to mouse movement
            RotationX = Mathf.Clamp(RotationX, MinVert, MaxVert); // fixating the angle with the range of min & max

            float rotationY = transform.localEulerAngles.y; // save angle without horiz rotation

            transform.localEulerAngles = new Vector3(RotationX, rotationY, 0); // create new vector from saved rotation data
        }
        else
        {
            //combined
            RotationX -= Input.GetAxis("Mouse Y") * SensivityVert;
            RotationX = Mathf.Clamp(RotationX, MinVert, MaxVert);

            float delta = Input.GetAxis("Mouse X") * SensivityHor; //amount of change in rotation angle
            float rotationY = transform.localEulerAngles.y + delta; //increment the angle with delta

            transform.localEulerAngles = new Vector3(RotationX, rotationY, 0);
        }
    }
}
