using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Security.Claims;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _popUpBottomText;
    [SerializeField] CanvasGroup _popUpBottom;
    [SerializeField] TextMeshProUGUI _popUpTopText;
    [SerializeField] CanvasGroup _popUpTop;
    [Space]
    [SerializeField] TextMeshProUGUI _timerText;
    [SerializeField] TextMeshProUGUI _nameText;
    [SerializeField] Image _waveDurationDisplay;
    [Space]
    [SerializeField] NameListSO _nameList;
    private Coroutine _bottomPopupCoroutine;
    private Coroutine _topPopupCoroutine;

    private bool _isOnWave = false;
    private float _currentWaveDuration = 0;
    private float _targetWaveDuration = 0;
    private float _timer = 0;
    private bool _isPopupDisplayed = false;

    public static UIManager Instance { get; private set; }
    public bool IsPopupDisplayed { get => _isPopupDisplayed;  }

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
    private void Start() 
    {
        var rNameIndex = UnityEngine.Random.Range(0,_nameList.Names.Length -1);   
        var currentPlayerData = PlayerController.Instance.CurrentPlayerData;
        var playerTypeText = currentPlayerData.GetString();

        if(playerTypeText != null)
        {
            var text = _nameList.Names[rNameIndex] +" "+ playerTypeText;
            DisplayPopUpText( new PopupText(text, 4f),false);
        }

        if(!string.IsNullOrEmpty(currentPlayerData.TypeName))
        {
            _nameText.text = "Subject:\n"+ _nameList.Names[rNameIndex] +"<size=80%>\n("+currentPlayerData.TypeName+")</size>";
        }
        else
        {
            _nameText.text = "Subject:\n"+ _nameList.Names[rNameIndex];
        }
    }

    private void Update() 
    {
        var timer = GameManager.Instance.SpeedRunTimer;
        string formattedTime = FormatTime(timer);

        // Function to format time into "00:00:000" format
        string FormatTime(float timeInSeconds)
        {
            int minutes = Mathf.FloorToInt(timeInSeconds / 60f);
            int seconds = Mathf.FloorToInt(timeInSeconds % 60f);
            int milliseconds = Mathf.FloorToInt((timeInSeconds - Mathf.Floor(timeInSeconds)) * 1000f);
            return string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
        }
        _timerText.text = formattedTime;

        if(_isOnWave)
        {
            var durationLeft = _targetWaveDuration - timer;
            var progress = 1f - (durationLeft / _currentWaveDuration);
            _waveDurationDisplay.fillAmount = progress;
            if(progress >1) _isOnWave = false;
        }
    }

    public void StartWaveDurationDisplay(float duration)
    {
        var timeOnWaveStart = GameManager.Instance.SpeedRunTimer;
        _currentWaveDuration = duration;
        _targetWaveDuration = timeOnWaveStart + _currentWaveDuration;
        _isOnWave = true;
    }
    

    public void DisplayPopUpText(PopupText textToDisplay, bool useBottom = true)
    {
        if(useBottom)
        {
            if(_bottomPopupCoroutine!= null)
                StopCoroutine(_bottomPopupCoroutine);
            
            _bottomPopupCoroutine = StartCoroutine(COR_PopupOnBottom(textToDisplay.text, textToDisplay.popupDuration));

        }
        else
        {
            if(_topPopupCoroutine!= null)
                StopCoroutine(_topPopupCoroutine);

            _topPopupCoroutine = StartCoroutine(COR_PopupOnTop(textToDisplay.text, textToDisplay.popupDuration));
        }
    }

    private IEnumerator COR_PopupOnBottom(string text, float duration)
    {
        _isPopupDisplayed = true;
        _popUpBottom.alpha = 1;
        _popUpBottomText.text = text;
        yield return new WaitForSeconds(duration);
        _popUpBottom.alpha = 0;
        _isPopupDisplayed = false;

    }

    private IEnumerator COR_PopupOnTop(string text, float duration)
    {
        // _isPopupDisplayed = true;
        _popUpTop.alpha = 1;
        _popUpTopText.text = text;
        yield return new WaitForSeconds(duration);
        _popUpTop.alpha = 0;
        // _isPopupDisplayed = false;

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

[TextArea(2,5)]
    public string text;
    public float popupDuration;

}
