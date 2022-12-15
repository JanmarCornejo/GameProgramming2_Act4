using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioSource _sfxSource;
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] AudioClip _bgMusic;

    [SerializeField] private Sound[] _soundData;
    private Dictionary<SoundType, Sound> _sounds = new Dictionary<SoundType, Sound>();

    protected override void Awake()
    {
        base.Awake();
        _sfxSource = GetComponent<AudioSource>();

        foreach (var s in _soundData)
        {
            _sounds[s.Type] = s;
        }
    }

    private void Start()
    {
        //PlaySound(_bgMusic);
        PlayMusic(_bgMusic);
    }

    public void PlaySound(AudioClip sound)
    {
        _sfxSource.PlayOneShot(sound);
    }

    public void PlaySound(SoundType type)
    {
        if (_sounds.TryGetValue(type, out Sound data))
        {
            PlaySound(data.Clip);
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        _musicSource.Stop();
        _musicSource.clip = clip;
        _musicSource.Play();
    }
}

[System.Serializable]
public class Sound
{
    public string Name;
    public SoundType Type;
    public AudioClip Clip;
}

public enum SoundType
{
    Unassigned,
    PlayerDeath,
    EnemyDeath,
    Teleport
}