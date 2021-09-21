using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{    
    [SerializeField] private Text scoreLabel;
    [SerializeField] private SettingsPopup settingsPopup;

    private int _score;

    void Start()
    {
        scoreLabel.text = _score.ToString(); //assign the score variable initial value 

        settingsPopup.Close();
    }
    void Awake() => Messenger.AddListener(GameEvent.ENEMY_HIT, OnEnemyHit); //declaring which method is responsible to the ENEMY_HIT event.
    
    void OnDestroy() => Messenger.RemoveListener(GameEvent.ENEMY_HIT, OnEnemyHit); //When the object is destroyed, we delete subscriber to avoid mistakes.

    private void OnEnemyHit()
    {
        _score += 1;
        scoreLabel.text = _score.ToString();
    }
    
    public void OnOpenSettings() => settingsPopup.Open();

    public void OnPointerDown() => Debug.Log("pointer down");
}
