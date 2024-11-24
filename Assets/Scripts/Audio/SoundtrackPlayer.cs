using System;
using UnityEngine;
using System.Collections.Generic;

public class SoundtrackPlayer : MonoBehaviour
{
    #region FIELDS
    [Header("Soundtrack")]
    [SerializeField] private List<AudioClip> _tracks = new();
    private AudioSource _audioSource;
    private System.Random _random = new();
    #endregion
    
    #region UNITY CALLBACKS
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        PlayRandomTrack();
    }

    private void Update()
    {
        if (!_audioSource.isPlaying)
            PlayRandomTrack();
    }

    #endregion
    
    #region CUSTOM METHODS
    public void PlayRandomTrack()
    {
        _audioSource.clip = _tracks[_random.Next(_tracks.Count)];
        if (!_audioSource.isPlaying)
            _audioSource.Play();
    }
    #endregion
}
