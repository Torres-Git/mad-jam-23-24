using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    [SerializeField] EntityBehaviour _entityPrefab;
    [SerializeField] int _entityAmount;
    [SerializeField] float _startHeight;
    [SerializeField] float _arenaEdge;
    [SerializeField] bool _noEntities;
    GameObject parent;



    public bool noEntities { get => _noEntities;  }
    // Start is called before the first frame update
    void Start()
    {
        parent = new GameObject("Entity Parent");
        DontDestroyOnLoad(parent);
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
        var e = Instantiate(entityData.entityPrefab, parent.transform);

        e.GetComponent<EntityBehaviour>().Spawn(entityData.startPos, entityData.startHeight);
    }
}
