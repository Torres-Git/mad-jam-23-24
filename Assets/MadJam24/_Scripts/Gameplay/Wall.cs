using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] Vector3 startPosition;
    [SerializeField] Vector3 endPosition;
    [SerializeField] float animDuration;
    [SerializeField] SimpleAudioEvent _audioWallMove;
    [SerializeField] AudioSource _audioSource;

    public void Setup(bool active)
    {
        if (active)
        {
            _audioWallMove.Play(_audioSource);
            transform.DOMove(endPosition, animDuration);
        }
        else
        {
            transform.DOMove(startPosition, animDuration);
        }
    }
}
