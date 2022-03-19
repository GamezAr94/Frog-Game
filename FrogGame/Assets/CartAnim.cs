using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartAnim : MonoBehaviour
{
    private Animator carAnimation;
    private void Awake()
    {
        carAnimation = this.GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        carAnimation.SetBool("isMoving", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
