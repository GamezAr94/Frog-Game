using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleted : MonoBehaviour
{
    [SerializeField]
    GameObject UILevelCompleted; 
    private void Awake() {
        UILevelCompleted.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        EventSystem.current.onCompletingLevel += showingEndingUIScreen;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showingEndingUIScreen(){
        UILevelCompleted.SetActive(true);
        Time.timeScale = 0;
    } 
}
