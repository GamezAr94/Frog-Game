using UnityEngine;

public enum UIElements{
    PlayBttn = 0,
    Bushes,
    Fly,
    Exclamation,
    Frog,
    PopUpUI,
    Button
}
public class LeanTweenAnimations : MonoBehaviour
{
    [SerializeField]
    UIElements elementType;
    [SerializeField]
    LeanTweenType easeType;
    [SerializeField][Range(0,1)]
    float scale;
    [SerializeField][Range(0.5f,6)]
    float duration, durationModifier;
    [SerializeField][Range(0f,2)]
    float delay;
    [SerializeField][Tooltip("a -1 value means infinite loop, anoter value (ie. 1) means that number of repetitions will be executed")]
    int loopCounter;
    
    [SerializeField]
    int movingOutX, movingOutY;
    void OnEnable()
    {
        if(elementType.Equals(UIElements.PlayBttn)){
            LeanTween.scale(gameObject, new Vector3(scale,scale,scale), duration).setLoopPingPong(loopCounter).setEase(easeType);
        }
        if(elementType.Equals(UIElements.PopUpUI)){
        }
        
        

    }
    private void Start()
    {
        float positionX = this.gameObject.transform.position.x;
        float positionY = this.gameObject.transform.position.y;
        if(elementType.Equals(UIElements.Bushes)){
            this.gameObject.transform.position = new Vector3(positionX+movingOutX, positionY, this.gameObject.transform.position.z);
            LeanTween.moveX(gameObject, positionX, duration).setDelay(delay).setEase(easeType);
        }
        if(elementType.Equals(UIElements.Fly)){
            LeanTween.moveX(gameObject, positionX+movingOutX, duration).setLoopPingPong(loopCounter).setEase(easeType);
            LeanTween.moveY(gameObject, positionY+movingOutY, duration*durationModifier).setLoopPingPong(loopCounter).setEase(easeType);
        }
        if(elementType.Equals(UIElements.Exclamation)){
            LeanTween.moveX(gameObject, positionX+movingOutX, duration).setLoopPingPong(loopCounter).setEase(easeType);
            LeanTween.moveY(gameObject, positionY+movingOutY, duration*durationModifier).setLoopPingPong(loopCounter).setEase(easeType);
            LeanTween.scale(gameObject, new Vector3(scale,scale,scale), duration).setLoopPingPong(loopCounter).setEase(easeType);
        }
        if(elementType.Equals(UIElements.Frog)){
            this.gameObject.transform.position = new Vector3(positionX, positionY+movingOutY, this.gameObject.transform.position.z);
            LeanTween.moveY(gameObject, positionY, duration).setDelay(delay).setEase(easeType);
        }

    }

    public void PopUpUI(){
        this.gameObject.transform.localScale = new Vector3(0,0,0);
        LeanTween.scale(gameObject, new Vector3(scale,scale,scale), duration).setDelay(delay).setEase(easeType).setIgnoreTimeScale(true);
    }
}
