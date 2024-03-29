using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityPillar : MonoBehaviour, IEntity
{
    [SerializeField] private float _startHeightOffset = 18;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private SimpleAudioEvent _audioOnSpawn;
    [SerializeField] private float spawnDuration;

    public void Spawn(Vector3 spawn) 
    { 
        transform.position = new Vector3(spawn.x, _startHeightOffset, spawn.z);
        transform.DOMoveY(spawn.y, spawnDuration).SetEase(Ease.Linear).OnComplete(()=>_audioOnSpawn.Play(_audioSource));
    }

    public void OnBulletImpact() 
    {
        DOTween.Complete(transform);
        transform.DOPunchScale(Vector3.one *.2f,.2f);
    }

    public bool IsDead()
    {
        return true;
    }

    public void RemoveEntity()
    {
        transform.DOMoveY(_startHeightOffset, spawnDuration).OnComplete(()=>Destroy(transform.gameObject));
    }
}
