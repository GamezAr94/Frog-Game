using UnityEngine;
using UnityEngine.U2D;

public class BodyTongBehaviour : MonoBehaviour
{
    SpriteShapeController _bodyTongSprite;
    public SpriteShapeController BodyTongSprite { get => _bodyTongSprite; }

    private int numberOfNodes;

    [SerializeField]
    GameObject tong;


    public int NumberOfNodes { get => numberOfNodes; private set => numberOfNodes = value; }

    private void Awake() {
        _bodyTongSprite = this.GetComponent<SpriteShapeController>();
        NumberOfNodes = _bodyTongSprite.spline.GetPointCount();
    }
    
    public void NodesFollowing() {
        //_bodyTongSprite.splineDetail = NumberOfNodes-1;
        _bodyTongSprite.spline.SetPosition(3,tong.transform.localPosition);
    }
}
