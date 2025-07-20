using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Signalization : MonoBehaviour
{
    private const float MinVolume = 0;
    private const float MaxVolume = 1;
    private const float TransitionDuration = 2f;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Trigger _trigger;

    private Coroutine _volumeRoutine;

    private void Awake()
    {
        _audioSource.volume = MinVolume;
    }

    private void OnEnable()
    {
        _trigger.TriggerEntered += OnTriggerEntered;
        _trigger.TriggerExited += OnTriggerExited;
    }

    private void OnDisable()
    {
        _trigger.TriggerEntered -= OnTriggerEntered;
        _trigger.TriggerExited -= OnTriggerExited;
    }

    private void OnTriggerEntered()
    {
        ChangeVolume(MaxVolume);
    }

    private void OnTriggerExited()
    {
        ChangeVolume(MinVolume);
    }

    private void ChangeVolume(float targetVolume)
    {
        if (_volumeRoutine != null)
            StopCoroutine(_volumeRoutine);
        
        _volumeRoutine = StartCoroutine(ChangeVolumeRoutine(targetVolume));
    }

    private IEnumerator ChangeVolumeRoutine(float targetVolume)
    {
        if (targetVolume > _audioSource.volume && !_audioSource.isPlaying)
            _audioSource.Play();

        var currentVolume = _audioSource.volume;
        var elapsed = 0f;

        while (!Mathf.Approximately(_audioSource.volume, targetVolume))
        {
            elapsed += Time.deltaTime;
            _audioSource.volume = Mathf.Lerp(currentVolume, targetVolume, elapsed / TransitionDuration);

            yield return null;
        }

        _audioSource.volume = targetVolume;

        if (Mathf.Approximately(targetVolume, MinVolume))
            _audioSource.Stop();
    }
}