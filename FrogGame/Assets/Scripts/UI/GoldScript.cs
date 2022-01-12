using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldScript : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI goldCoughtText;

    int goldCought = 0;

    
    //Here goes te gold cought script 
    private void Awake() {
    }

    // Start is called before the first frame update
    void Start()
    {
        EventSystem.current.onAddingGold += AddingGold;
        
    }

    void AddingGold(int gold){
        goldCought += gold; 
    } 

    // Update is called once per frame
    void Update()
    {
        goldCoughtText.text = goldCought.ToString();
    }
    public int getTotalGoldCought(){
        return goldCought;
    }
}
