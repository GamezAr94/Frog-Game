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
        EventSystem.current.onParallaxEffect += parallaxEffectController;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void parallaxEffectController(bool isStarting){
        if(isStarting){
            StartCoroutine(ParallaxEffect());
        }else{
            StopCoroutine(ParallaxEffect());
        }
    }

    IEnumerator ParallaxEffect(){
        while(true){
            rendererComponent.material.mainTextureOffset = new Vector2(0, Time.time * speed);
            yield return null;
        }
    }
}
