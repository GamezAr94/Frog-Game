using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderCallback : MonoBehaviour
{
    bool isFirstUpdate = true;

    // Update is called once per frame
    void Update()
    {
        if(isFirstUpdate){
            Debug.Log("Button move scene");
            isFirstUpdate = false;
            ButtonManager.LoaderCallback();
        }
    }
}
