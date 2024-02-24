using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletSO", menuName = "BulletSO")]

public class BulletSO : ScriptableObject
{
    [Header("Start Scale Anim")]
    [SerializeField] Vector3 _startBulletScale = Vector3.zero;
    [SerializeField] Vector3 _bulletScale = Vector3.one;
    [SerializeField] float _scaleAnimDuration = .3f;
    [SerializeField] Ease _startScaleEase = Ease.Unset;


    [Header("Bounce Scale Anim")]
    [SerializeField] float _bounceMultiplier = .4f;
    [SerializeField] float _bounceDuration = .4f;
    [SerializeField] Ease _bounceEase = Ease.Unset;



    [Header("Start Speed Stats")]
    [SerializeField] float _startSpeedMultiplier = 3f;
    [SerializeField] float _initialVelocityDuration = .3f;

    public Vector3 StartBulletScale { get => _startBulletScale; }
    public Vector3 BulletScale { get => _bulletScale;  }
    public float ScaleAnimDuration { get => _scaleAnimDuration;  }
    public Ease StartScaleEase { get => _startScaleEase; }
    public float BounceMultiplier { get => _bounceMultiplier; }
    public float BounceDuration { get => _bounceDuration;  }
    public Ease BounceEase { get => _bounceEase;  }
    public float StartSpeedMultiplier { get => _startSpeedMultiplier;  }
    public float InitialVelocityDuration { get => _initialVelocityDuration;  }
}
