using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IPlayerController
{

    [Header("Components")]
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private GameObject _model;
    [SerializeField] private InputReader _input;
    public Vector3 MoveInput { get => _inputVec;  }
    public GameObject Model { get => _model; }
    public Rigidbody Rb { get => _rb;  }
    

    [Header("Stats Base")]
    [SerializeField]  float _baseTurnSpeed = 360;
    // [SerializeField]private float _baseSpeed = 360;
     [SerializeField] float _currentSpeed;

    
    private bool _isMoving = false;
    private Quaternion _currentRotation;
    private bool _isInteracting = false;
    public bool IsMoving { get => _isMoving;  }
    public bool IsInteracting { get => _isInteracting;  }
    private Vector3 _inputVec;


    public static PlayerController Instance { get; private set; }
    public Quaternion CurrentRotation { get => _currentRotation; }

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

    private void OnEnable()
    {
        _input.MoveEvent += GatherMovInput;
        _input.FireEvent += GatherInteractInput;
    }

    private void OnDisable()
    {
        _input.MoveEvent -= GatherMovInput;
        _input.FireEvent -= GatherInteractInput;
    }


    private void FixedUpdate() 
    {
        Move();
    }
    private void LateUpdate() 
    {
        Look();
    }

    private void GatherMovInput(Vector2 newInput) 
    {
        _inputVec = new Vector3(newInput.x, 0, newInput.y);
    }
    private void GatherInteractInput(bool newInput) 
    {
        _isInteracting = newInput;
    }


    private void Look() 
    {
        if (_inputVec != Vector3.zero) 
        {
            _isMoving = true;
            _currentRotation = Quaternion.LookRotation(_inputVec.ToIso(), Vector3.up);
            _model.transform.rotation = Quaternion.RotateTowards(_model.transform.rotation, _currentRotation, _baseTurnSpeed * Time.deltaTime);
        }
        else
        {
            _isMoving =false;
        }
    }

    private void Move() 
    {
        _rb.MovePosition(transform.position + _model.transform.forward * _inputVec.normalized.magnitude  * _currentSpeed * Time.deltaTime);
    }
}

public static class Helpers 
{
    private static Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 0, 0));
    public static Vector3 ToIso(this Vector3 input) => _isoMatrix.MultiplyPoint3x4(input);
    public static Vector3 FromIso(Vector3 isoPos) => _isoMatrix.inverse.MultiplyPoint3x4(isoPos);    
}