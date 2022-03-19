using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frogAnim : MonoBehaviour
{
    public static Animator frogAnimation;

    private void Awake()
    {
        frogAnimation = this.GetComponent<Animator>();
    }

}
