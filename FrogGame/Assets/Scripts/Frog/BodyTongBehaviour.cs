using UnityEngine;
using UnityEngine.U2D;

public class BodyTongBehaviour : MonoBehaviour
{
    SpriteShapeController bodyTongSprite;
    private int numberOfNodes;
    public GameObject tong;

    public int NumberOfNodes { get => numberOfNodes; private set => numberOfNodes = value; }

    private void Awake() {
        bodyTongSprite = this.GetComponent<SpriteShapeController>();
        NumberOfNodes = bodyTongSprite.spline.GetPointCount();
    }
    private void Update() {
        bodyTongSprite.splineDetail = 3;
        bodyTongSprite.spline.SetPosition(3,tong.transform.localPosition);
    }
    
    public void followingNode(int node, Vector2 positionToFollow)
    {
        bodyTongSprite.spline.SetPosition(node, positionToFollow);
    }
}
