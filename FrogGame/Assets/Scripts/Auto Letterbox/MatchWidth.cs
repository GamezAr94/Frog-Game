using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchWidth : MonoBehaviour
{
    public float sceneWidth = 5.67f;
    private const float percentageOfReduction = 0.79432624f;

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

        //Percentage of reduction originaly was 0.5f and the virtual camera size was 5
        // on a 0.79432624 the VCam is 8.00
        // on a 0.7f the VCam is 7.05
        // on a 0.6f the Vcam is 6.05
        float desiredHalfHeight = percentageOfReduction * unitsPerPixel * Screen.height;

        _camera.orthographicSize = desiredHalfHeight;
    }
}
