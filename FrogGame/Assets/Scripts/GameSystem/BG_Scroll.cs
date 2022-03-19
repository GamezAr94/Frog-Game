using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_Scroll : MonoBehaviour
{
    [SerializeField]
    float speed;

    [SerializeField]
    Renderer rendererComponent;
    private void Awake() {
    }

    // Start is called before the first frame update
    void Start()
    {
        EventSystem.current.onStartMovingTheGame += ParallaxEffectController;
    }

    public void ParallaxEffectController(){ 
        StartCoroutine(ParallaxEffect());
    }

    private void OnDestroy()
    {
        StopCoroutine(ParallaxEffect());
        EventSystem.current.onStartMovingTheGame -= ParallaxEffectController;
    }

    IEnumerator ParallaxEffect(){
        while(true){
            rendererComponent.material.mainTextureOffset = new Vector2(0, Time.time * speed);
            yield return null;
        }
    }
}
