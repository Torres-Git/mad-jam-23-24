using UnityEngine;

[CreateAssetMenu(fileName = "Entity", menuName = "Entities")]
public class Entity : ScriptableObject
{
    [SerializeField] string _name;
    [SerializeField] Vector3 _startPos;
    [SerializeField] GameObject _entityPrefab;

    public Vector3 StartPos { get => _startPos; }
    public GameObject EntityPrefab { get => _entityPrefab; }
    public string Name { get => _name; }
}

public interface IEntity
{
    bool IsDead();
    void Spawn(Vector3 position);
    void OnBulletImpact();
    void RemoveEntity();
}
