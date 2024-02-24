using UnityEngine;

public interface IPlayerController 
{
    public Vector3 MoveInput { get; }
    public GameObject Model { get; }
    public Rigidbody Rb { get; }
    public bool IsMoving { get; }
}