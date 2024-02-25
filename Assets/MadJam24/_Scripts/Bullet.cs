using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class Bullet : MonoBehaviour
{
    [SerializeField] BulletSO _bulletData;
    [SerializeField] GameObject _playerHitParticles;
    [SerializeField] SimpleAudioEvent _audioPlayerHit, _audioWallHit;
    [SerializeField] AudioSource _audioSource;
    Rigidbody _rigidbody;
    bool _isReady = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>(); // Get the Rigidbody component
        _playerHitParticles.SetActive(false);
    }

    // Method to set the velocity and position of the bullet based on direction
public void SetPositionAndDirection(Transform rotationTransform, float speed)
{
    _isReady = false;
    transform.position = rotationTransform.position;

    // Calculate initial velocity
    Vector3 targetVelocity = rotationTransform.forward * speed;
    Vector3 initialVelocity = rotationTransform.forward * (speed * _bulletData.StartSpeedMultiplier);
    _rigidbody.velocity = initialVelocity;
    
    DOTween.Complete(transform);
    transform.localScale =  _bulletData.StartBulletScale;
    transform.DOScale(  _bulletData.BulletScale,  _bulletData.ScaleAnimDuration).SetEase( _bulletData.StartScaleEase);

    // Start coroutine to gradually reduce velocity
    StartCoroutine(GraduallyReduceVelocity( _bulletData.InitialVelocityDuration, targetVelocity));
}

private IEnumerator GraduallyReduceVelocity(float duration, Vector3 targetVelocity)
{
    float timer = 0f;
    Vector3 initialVelocity = _rigidbody.velocity;

    while (timer < duration)
    {
        if(_isReady) break; 

        // Linearly interpolate the velocity towards zero
        _rigidbody.velocity = Vector3.Lerp(initialVelocity, targetVelocity, timer / duration);
        // Increment timer
        timer += Time.deltaTime;
        yield return null;
    }

    _rigidbody.velocity = targetVelocity;
    _isReady = true;
}

    private void OnCollisionEnter(Collision other) 
    {
        var entity = other.gameObject.GetComponent<IEntity>();
        var player = other.gameObject.GetComponent<IPlayerController>();

        if(_isReady)
        {
            if(player != null)
            {
                _playerHitParticles.transform.position = player.Model.transform.position;
                player.Model.transform.localScale = Vector3.zero;
                _playerHitParticles.SetActive(true);

                _audioPlayerHit.Play(_audioSource);
                GameManager.Instance.RestartGame();
                transform.localScale = Vector3.zero;
            }
        }
        else
        {
            _isReady = true;
        }

        if(entity != null)
        {
            entity.OnBulletImpact();
        }

        if(entity == null && player == null)
            _audioWallHit.Play(_audioSource);

        DOTween.Complete(transform);
        transform.DOPunchScale(Vector3.one *  _bulletData.BounceMultiplier,  _bulletData.BounceDuration).SetEase( _bulletData.BounceEase);
    }
}
