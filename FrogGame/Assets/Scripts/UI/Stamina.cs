using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    [SerializeField]
    Slider sliderStaminaTime;

    float timer = 0;
    float waitTime = 2;
    
    // Start is called before the first frame update
    void Start()
    {
        EventSystem.current.onSettingStamina += DecrassingStamina;
    }

    private void DecrassingStamina(){
        sliderStaminaTime.value -= 15f;
    }

    private void Update() {
        timer += Time.deltaTime;
        if(timer > waitTime){
            //check that the slider cannot sum 49 + 5 because it will exced the max value
            if(sliderStaminaTime.value < 50){
                sliderStaminaTime.value += 5f;
            }
            timer = 0;
        }
    }
}
