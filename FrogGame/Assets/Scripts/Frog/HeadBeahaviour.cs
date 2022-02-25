using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBeahaviour : MonoBehaviour
{
    [Header("Looking At options")]

    [SerializeField]
    [Tooltip("Setting an object to look at, if it is null the frog wont look anything")]
    GameObject tarjetObject;


    private void Start() {
        EventSystem.current.onSettingHeadsRotation += SettingFrogsHeadRotation;
    }

    private void Update() {
        if(tarjetObject){
            //SettingFrogsHeadRotation(tarjetObject.transform.position);
        }
    }

    //function to set the right rotation of the head's frog
    public void SettingFrogsHeadRotation(Vector3 target)
    {
        target.z = 0;
        this.transform.up = target - transform.position;
    }
}
