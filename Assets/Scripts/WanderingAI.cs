using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI : MonoBehaviour
{
    public const float BaseSpeed = 3.0f;

    [SerializeField] private GameObject fireballPrefab;

    private GameObject _fireball;

    private bool _alive;

    public float Speed = 3.0f;
    public float ObstacleRange = 3.0f;
   
    void Awake() => Messenger<float>.AddListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);

    void Start() => _alive = true;

    void OnDestroy() => Messenger<float>.RemoveListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);

    private void OnSpeedChanged(float value) => Speed = BaseSpeed * value;

    public void SetAlive(bool alive) => _alive = alive;

    void Update()
    {
        if (_alive)
        {
            transform.Translate(0, 0, Speed * Time.deltaTime);

            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.SphereCast(ray, 0.75f, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                if (hitObject.GetComponent<PlayerCharacter>()) //identify player, like in Shooter.cs
                {
                    if (_fireball == null)
                    {
                        _fireball = Instantiate(fireballPrefab) as GameObject;
                        _fireball.transform.position = 
                            transform.TransformPoint(Vector3.forward * 1.5f); //place fireball in front of enemy
                        _fireball.transform.rotation = transform.rotation;
                    }
                }
                else if (hit.distance < ObstacleRange)
                {
                    float angle = Random.Range(-110, 110);
                    transform.Rotate(0, angle, 0);
                }
            }
        }
        
    }
}
