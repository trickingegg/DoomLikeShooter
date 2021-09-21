using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerCharacter))]
public class Fireball : MonoBehaviour
{
    public float Speed = 10.0f;
    public int Damage = 1;
    
    void Update() => transform.Translate(0, 0, Speed * Time.deltaTime);

    void OnTriggerEnter(Collider other)
    {
        PlayerCharacter player = other.GetComponent<PlayerCharacter>();
        if (player != null)
        {
            Debug.Log("Player hit");
        }
        Destroy(this.gameObject);
    }
}
