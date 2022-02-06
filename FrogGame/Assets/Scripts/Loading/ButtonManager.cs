using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class ButtonManager : MonoBehaviour
{

    [SerializeField]
    GameObject pauseButton;
    
    private static Action _onLoaderCallback;
    public void ButtonMoveScene(int level){
        Time.timeScale = 1;
        //set the loader callback action to load the target scene
        _onLoaderCallback = () => {SceneManager.LoadScene(level);};
        //Load the loading scene
        SceneManager.LoadScene("Loading");
    }

    public void RetryScene(){
        Time.timeScale = 1;
        //set the loader callback action to load the target scene
        _onLoaderCallback = () => {SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);};
        //Load the loading scene
        SceneManager.LoadScene("Loading");
    }

    public static void LoaderCallback(){
        // Triggered after the first Update which lets the screen refresh
        // Execute the loader callback action which will load the target scene
        if(_onLoaderCallback != null){
            _onLoaderCallback();
            _onLoaderCallback = null;
        }
    }
    public void EnableDisablePauseMenu(bool activate)
    {
        Time.timeScale = activate ? 0 : 1;
        //pauseMenu.SetActive(activate);
        pauseButton.SetActive(!activate);
    }
}
