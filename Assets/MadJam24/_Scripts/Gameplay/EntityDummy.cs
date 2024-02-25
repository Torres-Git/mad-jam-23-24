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

    public void Spawn(Vector3 spawn) 
    { 
        EnableRandomModel();
        RandomizeRotation();
        transform.position = new Vector3(spawn.x, _startHeightOffset, spawn.z);
        transform.DOMoveY(spawn.y, fallDuration);

    }
    
    public void OnBulletImpact() 
    {
        Destroy(gameObject);
    }

    private void EnableRandomModel()
    {
        int rModelIndex = Random.Range(0, _models.Length -1); 
        foreach (var item in _models)
        {
            item.gameObject.SetActive(false);
        }

        _models[rModelIndex].SetActive(true);
    }
    private void RandomizeRotation()
    {
        float yRotation = Random.Range(0f, 360f);
        Quaternion randomRotation = Quaternion.Euler(0f, yRotation, 0f);
        transform.rotation = randomRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            OnBulletImpact();
        }
    }




    private void OnTriggerEnter(Collider other) 
    {
        if(other.GetComponent<Bullet>()) OnBulletImpact();    
    }
}
