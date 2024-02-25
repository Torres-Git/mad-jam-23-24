using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    [SerializeField] bool _noEntities;
    GameObject parent;


    public bool noEntities { get => _noEntities;  }
    // Start is called before the first frame update
    void Start()
    {
        parent = new GameObject("Entity Parent");
    }

    // Update is called once per frame
    void Update()
    {
        if (parent.transform.childCount == 0)
        {
            _noEntities = true;
        }
    }

    public void InstantiateEntity(Entity entityData)
    {
        _noEntities = false;
        var e = Instantiate(entityData.EntityPrefab, parent.transform);
        e.GetComponent<IEntity>().Spawn(entityData.StartPos);
    }
}
