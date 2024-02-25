using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityDummy : MonoBehaviour, IEntity
{
    [SerializeField] private GameObject[] _models;

    [SerializeField] private float _startHeightOffset = 18;
    [SerializeField] private bool isDead = false;
    [SerializeField] private float fallDuration;
    private int _rModelIndex;

    public void Spawn(Vector3 spawn) 
    { 
        EnableRandomModel();
        RandomizeRotation();
        transform.position = new Vector3(spawn.x, _startHeightOffset, spawn.z);
        transform.DOMoveY(spawn.y, fallDuration);
    }
    
    public void OnBulletImpact() 
    {
        isDead = true;
        transform.DOScale(Vector3.one * .1f, 1f).OnComplete(
            ()=>
            { 
                _models[_rModelIndex].gameObject.SetActive(false);
            });
    }

    public void RemoveEntity()
    {
        Destroy(transform.gameObject);
    }

    public bool IsDead()
    {
       return isDead;
    }

    private void EnableRandomModel()
    {
       _rModelIndex = Random.Range(0, _models.Length -1); 
        foreach (var item in _models)
        {
            item.gameObject.SetActive(false);
        }

        _models[_rModelIndex].SetActive(true);
    }
    private void RandomizeRotation()
    {
        float yRotation = Random.Range(0f, 360f);
        Quaternion randomRotation = Quaternion.Euler(0f, yRotation, 0f);
        transform.rotation = randomRotation;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.GetComponent<Bullet>()) OnBulletImpact();    
    }


    
}
