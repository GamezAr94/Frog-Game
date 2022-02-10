using UnityEngine;

public class renderTong : MonoBehaviour
{
    LineRenderer _renderLine;

    [SerializeField]
    private GameObject pivotOriginPoint; 
    [SerializeField]
    private GameObject tipTong; 
    // Start is called before the first frame update
    private void Awake()
    {
        _renderLine = this.GetComponent<LineRenderer>();
        _renderLine.positionCount = 2;
    }

    void LateUpdate()
    {
        _renderLine.SetPosition(0, pivotOriginPoint.transform.position);
        _renderLine.SetPosition(1, tipTong.transform.position);
    }
}
