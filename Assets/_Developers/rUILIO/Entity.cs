using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Entity", menuName = "Entities")]
public class Entity : ScriptableObject
{
    [SerializeField] Vector3 _startPos;
    [SerializeField] GameObject _entityPrefab;
    [SerializeField] float _startHeight;

    public Vector3 startPos { get => _startPos; }
    public GameObject entityPrefab { get => _entityPrefab; }
    public float startHeight { get => _startHeight; }
}

public interface IEntity
{
    void DestroyEntity();
}
