using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField]
    GameObject pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void EnableDisablePauseMenu(bool activate){
        if (activate){ 
            Time.timeScale = 0;
        } else{ 
            Time.timeScale = 1;
        }
        pauseMenu.SetActive(activate);
    }

    public void ExitMainMuenu(){
        
    }
}
