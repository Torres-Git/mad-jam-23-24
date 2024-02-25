using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;

public class BlasterBehaviour : MonoBehaviour
{    
    private const string  FIRE_TRIGGER = "OnFire";
    [SerializeField] InputReader _input;
    [SerializeField] Animator _animator;
    [SerializeField] Transform _gunModel;
    [Space]
    [SerializeField] MeshRenderer[] _coolDownMeshes;
    [SerializeField] Material _coolDownMaterial;
    [SerializeField] Material _readyMaterial;
    [Space]
    [SerializeField] SimpleAudioEvent _audioOnFire;
    [SerializeField] AudioSource _audioSource;
    [Space]
    [SerializeField] Bullet _bulletPrefab; // Reference to the bullet prefab
    [SerializeField] float _bulletSpeed = 10;
    [SerializeField] float _gunCooldownInSeconds = 5f;
    private WaitForSeconds _gunCooldownYeilder;
    [SerializeField] bool _canFire = true;

    Stack<Bullet> _bulletStack; // A stack to hold the inactive bullets
    Bullet _lastBullet; // Reference to the last bullet fired

    void Start()
    {
        _bulletStack = new Stack<Bullet>(); // Initialize the stack
    }

    private void OnEnable() 
    {
        _input.FireStartEvent += FireRequest;    
    }
    private void OnDisable() 
    {
        _input.FireStartEvent -= FireRequest;   
    }

    // Function to request firing a bullet
    void FireRequest()
    {
        if(!_canFire) return;

        Bullet bulletInstance = null;

        if (_bulletStack.Count > 0)
        {
            // If there are inactive bullets in the pool, reuse one
            bulletInstance = _bulletStack.Pop();
            bulletInstance.gameObject.SetActive(true); // Activate the bullet
        }
        else
        {
            // If the pool is empty, instantiate a new bullet
            bulletInstance = Instantiate(_bulletPrefab);
        }

        bulletInstance.SetPositionAndDirection(_gunModel,_bulletSpeed);
        _animator.SetTrigger(FIRE_TRIGGER);
        _lastBullet = bulletInstance; // Update the reference to the last fired bullet
        _audioOnFire.Play(_audioSource);
        StartCoroutine(COR_GunCooldown());
    }

    // Function to return a bullet back to the pool
    void ReturnBulletToPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(false); // Deactivate the bullet
        _bulletStack.Push(bullet); // Add the bullet back to the stack
    }

    private IEnumerator COR_GunCooldown()
    {
        _canFire = false;
        foreach (var item in _coolDownMeshes)
        {
            item.material = _coolDownMaterial;
        }
        yield return new WaitForSeconds(_gunCooldownInSeconds);

        foreach (var item in _coolDownMeshes)
        {
            item.material = _readyMaterial;
        }
        _canFire = true;
    }

        private void OnDrawGizmosSelected()
    {
        if (_gunModel != null)
        {
            Gizmos.color = Color.red;
        
            Gizmos.DrawRay(transform.position, _gunModel.forward * 5f); // Change 5f to the length you desire

        }
    }
}
