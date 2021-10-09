using UnityEngine;

public class LineScreenDraw : MonoBehaviour
{
    LineRenderer renderLine;
    private void Awake()
    {
        renderLine = this.GetComponent<LineRenderer>();
        renderLine.positionCount = 2;
    }

// Function to draw the unser input drag touch
    public void setDrawPosition(int lineNum = 2, float positionX = 0, float positionY = 0)
    {
        if (lineNum == 2)
        {
            renderLine.SetPosition(0, new Vector3(positionX, positionY, 0f));
            renderLine.SetPosition(1, new Vector3(positionX, positionY, 0f));
        }
        else if (lineNum == 0)
        {
            renderLine.SetPosition(lineNum, new Vector3(positionX, positionY, 0f));
        }
        else if (lineNum == 1)
        {
            renderLine.SetPosition(lineNum, new Vector3(positionX, positionY, 0f));
        }
        else
        {
            Debug.LogError("Wrong LineNum Input. Input must be a number between 0 and 2 inclusive. Your Inut = " + lineNum);
        }
    }
}
