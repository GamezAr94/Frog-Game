using UnityEngine;
using UnityEngine.U2D;

public class BodyTongBehaviour : MonoBehaviour
{
    
    [Header("Body Tong Behaviour Settings")]

    [Tooltip("Component needed to maipulate and strech the sprite")]
    SpriteShapeController _bodyTongSprite;

    int numberOfNodes;

    [SerializeField]
    [Tooltip("Tong Game Object to get its position")]
    GameObject tong;

    private void Awake() {
        _bodyTongSprite = this.GetComponent<SpriteShapeController>();
        numberOfNodes = _bodyTongSprite.spline.GetPointCount();
    }
    
    //Function to set the position of the last node of the body tong to the position of the tong
    public void NodesFollowing() {
        _bodyTongSprite.spline.SetPosition(numberOfNodes-1,tong.transform.localPosition);
    }
}
