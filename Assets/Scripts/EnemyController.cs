using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public const float BaseSpeed = 3.0f;

    [SerializeField] private GameObject _enemyPrefab;

    private GameObject _enemy;

    public float Speed = 3.0f;

    void Awake() => Messenger<float>.AddListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);

    void OnDestroy() => Messenger<float>.RemoveListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);

    private void OnSpeedChanged(float value) => Speed = BaseSpeed * value;

    void Update()
    {
        if (_enemy == null)
        {
            _enemy = Instantiate(_enemyPrefab) as GameObject; // method which copies asset
            _enemy.transform.position = new Vector3(0, 1, 0);
            float angle = Random.Range(0, 360);
            _enemy.transform.Rotate(0, angle, 0);
        }
    }
}
