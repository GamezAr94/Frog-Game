using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class ButtonManager : MonoBehaviour
{
    private static Action onLoaderCallback;
    public void ButtonMoveScene(int level){
        //set the loader callback action to load the target scene
        onLoaderCallback = () => {SceneManager.LoadScene(level);};
        //Load the loading scene
        SceneManager.LoadScene("Loading");
    }

    public static void LoaderCallback(){
        // Triggered after the first Udate which lets the screen refresh
        // Execute the loader callback ation which will load the target scene
        if(onLoaderCallback != null){
            onLoaderCallback();
            onLoaderCallback = null;
        }
    }
}
