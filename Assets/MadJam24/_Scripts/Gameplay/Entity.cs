using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Entity", menuName = "Entities")]
public class Entity : ScriptableObject
{
    [SerializeField] string _name;
    [SerializeField] Vector3 _startPos;
    [SerializeField] GameObject _entityPrefab;

    public Vector3 StartPos { get => _startPos; }
    public GameObject EntityPrefab { get => _entityPrefab; }
    public string Name { get => _name; }


    public UnityAction DrawGizmo()
    {
        return new UnityAction(() =>
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(_startPos, 0.05f);
        });
    }
}

public interface IEntity
{
    void Spawn(Vector3 position);
    void OnBulletImpact();
}
