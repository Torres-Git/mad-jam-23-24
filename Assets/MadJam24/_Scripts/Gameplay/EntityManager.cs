using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    [SerializeField] List<GameObject> _currentEntities;

    public bool AreAllEntitiesDead()
    {
        foreach (var item in _currentEntities)
        {
            if(item.GetComponent<IEntity>().IsDead() == false) return false;
        }

        CleanCurrentEntities();
        return true;
    }

    public void CleanCurrentEntities()
    {
        for (int i = _currentEntities.Count - 1; i >= 0; i--)
        {
            GameObject item = _currentEntities[i];
            item.GetComponent<IEntity>().RemoveEntity();
        }
        _currentEntities.Clear();
    }

    public void InstantiateEntity(Entity entityData)
    {
        var e = Instantiate(entityData.EntityPrefab, this.transform);
        var entityBehaviour =  e.GetComponent<IEntity>();
        entityBehaviour.Spawn(entityData.StartPos);

        _currentEntities.Add(e);
    }
}
