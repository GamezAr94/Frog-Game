using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_Scroll : MonoBehaviour
{
    [SerializeField]
    float speed = -0.1f;

    [SerializeField]
    Renderer rendererComponent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rendererComponent.material.mainTextureOffset = new Vector2(0, Time.time * speed);
    }
}
