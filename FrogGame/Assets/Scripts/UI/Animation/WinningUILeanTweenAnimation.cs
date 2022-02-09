using System;
using UnityEngine;
using UnityEngine.UI;

public class WinningUILeanTweenAnimation : MonoBehaviour
{
    [SerializeField] private LeanTweenType screenUIEase;
    [SerializeField] private float screenUIDuration;
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject content;
    [SerializeField] private GameObject[] stars;

    private void Start()
    {
        EventSystem.current.onCompletingLevel += ShowingWinningUI;
        LeanTween.alpha(background.gameObject.transform.GetComponent<Image>().rectTransform, -10f, 0f);
        LeanTween.moveLocalY(content, 1500, .2f);
    }

    void ShowingWinningUI()
    {
        LeanTween.alpha(background.transform.GetComponent<Image>().rectTransform, .6f, screenUIDuration).setIgnoreTimeScale(true);
        LeanTween.moveLocalY(content, 0, .2f).setEase(screenUIEase);
        EventSystem.current.onCompletingLevel -= ShowingWinningUI;
    }
    
}
