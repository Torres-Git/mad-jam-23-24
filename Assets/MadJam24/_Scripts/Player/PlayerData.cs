using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerData")]
public class PlayerData : ScriptableObject
{
    [SerializeField] string _typeName;
    [SerializeField] string[] _playerTypeStrings;
    [Space]
    [SerializeField] float _rotationSpeed;
    [SerializeField] float _speed;
    [SerializeField] float _gunCooldown;
    [SerializeField] float _bulletSpeed;
    [SerializeField] bool _isTriggerHappy;
    [SerializeField] Vector3 _bulletScaleOverride = Vector3.zero;

    public string GetString()
    {
        if(_playerTypeStrings.Length<=0) return null;

        return _playerTypeStrings[Random.Range(0,_playerTypeStrings.Length)];
    }

    public float RotationSpeed { get => _rotationSpeed;  }
    public float Speed { get => _speed;  }
    public float GunCooldown { get => _gunCooldown;  }
    public bool IsTriggerHappy { get => _isTriggerHappy;  }
    public float BulletSpeed { get => _bulletSpeed;  }
    public Vector3 BulletScaleOverride { get => _bulletScaleOverride;}
    public string[] PlayerTypeStrings { get => _playerTypeStrings;  }
    public string TypeName { get => _typeName;  }
}
