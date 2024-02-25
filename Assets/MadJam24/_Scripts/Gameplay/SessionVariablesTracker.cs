using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionVariablesTracker : MonoBehaviour
{
    [SerializeField] bool _hasPlayed = false;
    public static SessionVariablesTracker Instance { get; private set; }
    public bool HasPlayed { get => _hasPlayed; set => _hasPlayed = value; }

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

        _hasPlayed = false;
        DontDestroyOnLoad(this);
    }


}
