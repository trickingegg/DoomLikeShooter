using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]
public class FPSInput : MonoBehaviour
{
    public const float BaseSpeed = 6.0f;

    private CharacterController _charController;

    public float Speed = 6.0f;
    public float Gravity = -9.8f;


    void Awake() => Messenger<float>.AddListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);

    void OnDestroy() => Messenger<float>.RemoveListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);

    private void OnSpeedChanged(float value) => Speed = BaseSpeed * value;

    void Start() => _charController = GetComponent<CharacterController>();

    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * Speed;
        float deltaZ = Input.GetAxis("Vertical") * Speed;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, Speed);
        movement.y = Gravity;
        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        _charController.Move(movement);
    }
}
