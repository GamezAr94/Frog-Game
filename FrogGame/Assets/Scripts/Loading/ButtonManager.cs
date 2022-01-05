using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class ButtonManager : MonoBehaviour
{
    [SerializeField]
    GameObject pauseMenu;

    [SerializeField]
    GameObject pauseButton;
    
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
    public void EnableDisablePauseMenu(bool activate){
        if (activate){ 
            Time.timeScale = 0;
        } else{ 
            Time.timeScale = 1;
        }
        pauseMenu.SetActive(activate);
        pauseButton.SetActive(!activate);
    }
}
