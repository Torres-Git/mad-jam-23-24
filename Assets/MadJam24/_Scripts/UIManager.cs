using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField] CanvasGroup _popUp;
    [SerializeField] TextMeshProUGUI _popUpText;
    [SerializeField] TextMeshProUGUI _timerText;
    private Coroutine _popupCoroutine;
    public static UIManager Instance { get; private set; }
    
    private void Awake() 
    { 
        // If there is an instance, and it's not me, delete myself.
        
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    private void Update() 
    {
        string formattedTime = FormatTime(GameManager.Instance.SpeedRunTimer);

        // Function to format time into "00:00:000" format
        string FormatTime(float timeInSeconds)
        {
            int minutes = Mathf.FloorToInt(timeInSeconds / 60f);
            int seconds = Mathf.FloorToInt(timeInSeconds % 60f);
            int milliseconds = Mathf.FloorToInt((timeInSeconds - Mathf.Floor(timeInSeconds)) * 1000f);
            return string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
        }
        _timerText.text = formattedTime;

    }
    public void DisplayPopUpText(PopupText textToDisplay)
    {
        if(_popupCoroutine!= null)
            StopCoroutine(_popupCoroutine);

        _popupCoroutine = StartCoroutine(COR_Popup(textToDisplay.text, textToDisplay.popupDuration));
    }

    private IEnumerator COR_Popup(string text, float duration)
    {
        _popUp.alpha = 1;
        _popUpText.text = text;
        yield return new WaitForSecondsRealtime(duration);
        _popUp.alpha = 0;

    }
}

[Serializable]
public class PopupText
{
    public PopupText(string v1, float v2)
    {
        this.text = v1;
        this.popupDuration = v2;
    }

    public string text;
    public float popupDuration;

}
