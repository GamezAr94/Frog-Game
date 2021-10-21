using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [Header("Player Action Links")]

    [Tooltip("Component to retrieve the script of the tong behaviour")]
    [SerializeField]
    TongBehaviour tongBehaviour;

    LineScreenDraw lineRendererObject;

    private void Awake()
    {
        lineRendererObject = this.GetComponent<LineScreenDraw>();
    }

    public void Update() {
        if(!tongBehaviour.TongInMouth){//If the tong is not in mouth and the frog move, it will look at the target position of the first user input touch
            SettingFrogsHeadRotation(tongBehaviour.ThisPosition);
        }
    }

//function to set the right rotation of the head's frog
    public void SettingFrogsHeadRotation(Vector3 target){
        transform.right = target - transform.position;
    }

}
