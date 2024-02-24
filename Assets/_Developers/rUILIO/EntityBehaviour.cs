using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBehaviour : MonoBehaviour, IEntity
{
    [SerializeField] private bool isDead = false;
    [SerializeField] private float fallDuration;
    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            DestroyEntity();
        }
    }

    public void Spawn(Vector3 spawn, float height) 
    { 
        transform.position = new Vector3(spawn.x, height, spawn.z);

        transform.DOMoveY(spawn.y, fallDuration);
    }

    public void DestroyEntity() 
    {
        Destroy(gameObject);
    }
}
