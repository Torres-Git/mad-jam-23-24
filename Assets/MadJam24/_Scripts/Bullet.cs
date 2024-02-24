using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class Bullet : MonoBehaviour
{
    [SerializeField] BulletSO _bulletData;
    Rigidbody _rigidbody;
    bool _isReady = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>(); // Get the Rigidbody component
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
        if(_isReady)
        {
            if(other.gameObject.GetComponent<IPlayerController>() != null)
            {
                // GameManager...
                var s = SceneManager.GetActiveScene();
                SceneManager.LoadScene(s.name);
            }
        }
        else
        {
            _isReady = true;
        }
        
        DOTween.Complete(transform);
        transform.DOPunchScale(Vector3.one *  _bulletData.BounceMultiplier,  _bulletData.BounceDuration).SetEase( _bulletData.BounceEase);
    }
}
