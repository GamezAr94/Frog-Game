using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartAnim : MonoBehaviour
{
    Animator carAnimation;
    [SerializeField] private GameObject dustParticles;
    
    private void Awake()
    {
        carAnimation = this.GetComponent<Animator>();
        EventSystem.current.onStartMovingTheGame += StartingAnimation;
    }

    void StartingAnimation()
    {
        carAnimation.SetBool("isMoving", true);
        dustParticles.SetActive(true);
    }
}
