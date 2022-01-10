using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldScript : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI goldCought;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        goldCought.text = GameManager.goldCought.ToString();
    }
    public int getTotalGoldCought(){
        return GameManager.goldCought;
    }
}
