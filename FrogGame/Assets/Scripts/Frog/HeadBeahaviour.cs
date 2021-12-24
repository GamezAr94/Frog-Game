using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBeahaviour : MonoBehaviour
{
    private void Start() {
        EventSystem.current.onSettingHeadsRotation += SettingFrogsHeadRotation;
    }

    //function to set the right rotation of the head's frog
    public void SettingFrogsHeadRotation(Vector3 target){
        transform.right = target - transform.position;
    }
}
