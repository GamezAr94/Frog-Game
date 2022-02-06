using UnityEngine;
using UnityEngine.UI;

public enum UIElements{
    PlayBtn = 0,
    Bushes,
    Fly,
    Exclamation,
    Frog,
    PopUpUI,
    BackgroundFade,
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
    [SerializeField][Tooltip("a -1 value means infinite loop, another value (ie. 1) means that number of repetitions will be executed")]
    int loopCounter;
    [SerializeField]
    int movingOutX, movingOutY;

    void OnEnable()
    {
        if(elementType.Equals(UIElements.PlayBtn)){
            LeanTween.scale(gameObject, new Vector3(scale,scale,scale), duration).setLoopPingPong(loopCounter).setEase(easeType);
        }
        if(elementType.Equals(UIElements.PopUpUI)){
            this.gameObject.transform.localScale = new Vector3(0,0,0);
            LeanTween.scale(gameObject, new Vector3(scale,scale,scale), duration).setDelay(delay).setEase(easeType).setIgnoreTimeScale(true);
        }
        if(elementType.Equals((UIElements.BackgroundFade)))
        {
            Image objectToFade = this.gameObject.transform.GetComponent<Image>();
            LeanTween.alpha(objectToFade.rectTransform, 0, .3f);
        }
    }

    private void Start()
    {
        Vector3 position = gameObject.transform.position;
        switch (elementType)
        {
            case UIElements.Bushes:
                this.transform.position = new Vector3(position.x+movingOutX, position.y, this.gameObject.transform.position.z);
                LeanTween.moveX(gameObject, position.x, duration).setDelay(delay).setEase(easeType);
                break;
            case UIElements.Fly:
                LeanTween.moveX(gameObject, position.x+movingOutX, duration).setLoopPingPong(loopCounter).setEase(easeType);
                LeanTween.moveY(gameObject, position.y+movingOutY, duration*durationModifier).setLoopPingPong(loopCounter).setEase(easeType);
                break;
            case UIElements.Exclamation:
                LeanTween.moveX(gameObject, position.x+movingOutX, duration).setLoopPingPong(loopCounter).setEase(easeType);
                LeanTween.moveY(gameObject, position.y+movingOutY, duration*durationModifier).setLoopPingPong(loopCounter).setEase(easeType);
                LeanTween.scale(gameObject, new Vector3(scale,scale,scale), duration).setLoopPingPong(loopCounter).setEase(easeType);
                break;
            case UIElements.Frog:
                this.transform.position = new Vector3(position.x, position.y+movingOutY, this.gameObject.transform.position.z);
                LeanTween.moveY(gameObject, position.y, duration).setDelay(delay).setEase(easeType);
                break;
        }
    }

    public void Fade(int value)
    {
        Image objectToFade = this.gameObject.transform.GetComponent<Image>();
        LeanTween.alpha(objectToFade.rectTransform, value, .3f);
    }
    public void PopUpUI(){
        this.transform.localScale = new Vector3(0,0,0);
        LeanTween.scale(gameObject, new Vector3(scale,scale,scale), duration).setDelay(delay).setEase(easeType).setIgnoreTimeScale(true);
    }
    public void PopOutUI(){
        LeanTween.scale(gameObject, new Vector3(0,0,0), .2f).setDelay(delay).setEase(LeanTweenType.linear).setIgnoreTimeScale(true);
    }
}
