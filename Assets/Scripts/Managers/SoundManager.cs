using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : Singleton<SoundManager>
{
    private AudioSource source;
    [SerializeField] AudioClip BackgroundMusic;

    [SerializeField] private Sound[] _soundData;
    private Dictionary<SoundType, Sound> _sounds = new Dictionary<SoundType, Sound>();

    protected override void Awake()
    {
        base.Awake();
        source = GetComponent<AudioSource>();

        foreach (var s in _soundData)
        {
            _sounds[s.Type] = s;
        }
    }

    private void Start()
    {
        PlaySound(BackgroundMusic);
    }

    public void PlaySound(AudioClip sound)
    {
        source.PlayOneShot(sound);
    }

    public void PlaySound(SoundType type)
    {
        if (_sounds.TryGetValue(type, out Sound data))
        {
            PlaySound(data.Clip);
        }
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