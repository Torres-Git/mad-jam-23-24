using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] bool _isGameRestartableInEditor = true;
    [SerializeField] float _speedRunTimer;
    bool _isCounting = true;
    public static GameManager Instance { get; private set; }
    public float SpeedRunTimer { get => _speedRunTimer; }

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
        if(_isCounting)
            _speedRunTimer += Time.deltaTime;
    }
    

    // Update is called once per frame
    public void  RestartGame()
    {
        #if UNITY_EDITOR
            if(!_isGameRestartableInEditor) return;
        #endif
        UIManager.Instance.DisplayPopUpText(new PopupText("!SUBJECT DISCARDED!", 1f));    
        Time.timeScale =.2f;
        _isCounting =false;
        StartCoroutine(COR_DelayRestart());

    }

    public void WinGame()
    {
        UIManager.Instance.DisplayPopUpText(new PopupText("GG Ez!", 1f));    
        Time.timeScale = 0f;
    }

    IEnumerator COR_DelayRestart()
    {
        yield return new WaitForSecondsRealtime(1f);
         Time.timeScale =1f;
         _isCounting = true;
        var s = SceneManager.GetActiveScene();
        SceneManager.LoadScene(s.name);
    }
}
