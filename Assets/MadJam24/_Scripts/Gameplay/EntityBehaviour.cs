using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBehaviour : MonoBehaviour, IEntity
{
    [SerializeField] private bool isDead = false;
    [SerializeField] private float fallDuration;

    public void Spawn(Vector3 spawn, float height) 
    { 
        transform.position = new Vector3(spawn.x, height, spawn.z);

        transform.DOMoveY(spawn.y, fallDuration);
    }


    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            OnBulletImpact();
        }
    }


    public void OnBulletImpact() 
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.GetComponent<Bullet>()) OnBulletImpact();    
    }
}
