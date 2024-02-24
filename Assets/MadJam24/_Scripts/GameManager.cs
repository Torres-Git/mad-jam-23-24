using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] bool _isGameRestartableInEditor = true;
    [SerializeField] float _speedRunTimer;
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
        _speedRunTimer += Time.deltaTime;
    }
    
    private void Start() 
    {
        UIManager.Instance.DisplayPopUpText(new PopupText("Have a nice day!", 1f));    
    }

    // Update is called once per frame
    public void  RestartGame()
    {
        #if UNITY_EDITOR
            if(!_isGameRestartableInEditor) return;
        #endif

        var s = SceneManager.GetActiveScene();
        SceneManager.LoadScene(s.name);
        
    }
}
