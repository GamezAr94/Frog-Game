using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HeadBehaviour : MonoBehaviour
{
    Animator headMovemenAtttack;
    [SerializeField]
    Animator blinkingAnim;
    private void Awake()
    {
        headMovemenAtttack = this.GetComponent<Animator>();
    }

    public void playHeadAnimations(bool tongInMouth)
    {
        // headMovemenAtttack.SetBool("HeadAttack", tongInMouth);
        // blinkingAnim.SetBool("isBlinking", tongInMouth);
    }
}
