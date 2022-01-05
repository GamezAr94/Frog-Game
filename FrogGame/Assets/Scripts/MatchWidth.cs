using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchWidth : MonoBehaviour
{
    public float sceneWidth = 5.67f;

    Camera _camera;
    void Start() {
        _camera = GetComponent<Camera>();
    }

    // Adjust the camera's height so the desired scene width fits in view
    // even if the screen/window size changes dynamically.
    void Update() {
        if(Screen.height - Screen.width < 500){
            sceneWidth = 8f;
        }else if(Screen.height - Screen.width < 700 && Screen.height - Screen.width > 500){
            sceneWidth = 7.5f;
        }
        else{
            sceneWidth = 5.67f;
        }
        float unitsPerPixel = sceneWidth / Screen.width;
        //Debug.Log(Screen.width);
        //Debug.Log(Screen.height);
        float desiredHalfHeight = 0.5f * unitsPerPixel * Screen.height;

        _camera.orthographicSize = desiredHalfHeight;
    }
}
