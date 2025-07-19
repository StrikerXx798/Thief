using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider), typeof(AudioSource))]
public class Signalization : MonoBehaviour
{
    private const float MinVolume = 0;
    private const float MaxVolume = 1;
    private const float TransitionDuration = 2f;

    [SerializeField] private AudioSource _audioSource;

    private bool _isTriggered;

    private void Awake()
    {
        _audioSource.volume = MinVolume;
    }

    private void Update()
    {
        var speed = (_audioSource.volume - MaxVolume) / TransitionDuration;
        var delta = Mathf.Abs(speed) * Time.deltaTime;

        _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _isTriggered ? MaxVolume : MinVolume, delta);

        if (Mathf.Approximately(_audioSource.volume, MinVolume))
        {
            _audioSource.Stop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _isTriggered = true;

        if (_audioSource.isPlaying == false)
        {
            _audioSource.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _isTriggered = false;
    }
}