using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Shooter : MonoBehaviour
{
    private Camera _camera;
    
    void Start()
    {
        _camera = GetComponent<Camera>();

        //Cursor.lockState = CursorLockMode.Locked; //Hide Cursor
        Cursor.visible = false;
    }

    void OnGUI()
    {
        int size = 20;
        float posX = _camera.pixelWidth / 2 - size / 4;
        float posY = _camera.pixelHeight / 2 - size / 2;
        GUI.Label(new Rect(posX, posY, size, size), "+"); //GUI.Label shows symbol on screen
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0) &&
            !EventSystem.current.IsPointerOverGameObject()) //reaction to mouse click
        {
            Vector3 point = new Vector3(
                _camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
            Ray ray = _camera.ScreenPointToRay(point); //create ray in the middle of screen
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();
                if (target != null)
                {
                    target.ReactToHit();
                    Messenger.Broadcast(GameEvent.ENEMY_HIT);
                }
                else
                {
                    StartCoroutine(SphereIndicator(hit.point)); //start the program in respponse to hit
                }
                
            }
        }
    }
    private IEnumerator SphereIndicator(Vector3 pos)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = pos;

        yield return new WaitForSeconds(1);

        Destroy(sphere);
    }
}
