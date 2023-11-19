using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{
    //reference: https://ansohxxn.github.io/unity%20lesson%202/ch9-1/
    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount,  // 아무것도 아님. 그냥 Sound enum의 개수 세기 위해 추가. (0, 1, '2' 이렇게 2개) 
    }
    AudioSource[] _audioSources = new AudioSource[(int)Sound.MaxCount];
    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

    public void Start()
    {
        Init();
    }
    public void Init()
    {
        GameObject root = GameObject.Find("@Sound");
        if (root == null)
        {
            root = new GameObject { name = "@Sound" };
            Object.DontDestroyOnLoad(root);

            string[] soundNames = System.Enum.GetNames(typeof(Sound)); // "Bgm", "Effect"
            for (int i = 0; i < soundNames.Length - 1; i++)
            {
                GameObject go = new GameObject { name = soundNames[i] };
                _audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }

            _audioSources[(int)Sound.Bgm].volume = 0.5f;

        }


    }
    public void Clear()
    {
        // 재생기 전부 재생 스탑, 음반 빼기
        foreach (AudioSource audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        // 효과음 Dictionary 비우기
        _audioClips.Clear();
    }

    public void Play(AudioClip audioClip, Sound type = Sound.Effect, float pitch = 1.0f)
    {
        if (audioClip == null)
            return;

        if (type == Sound.Bgm) // BGM 배경음악 재생
        {
            AudioSource audioSource = _audioSources[(int)Sound.Bgm];
            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else // Effect 효과음 재생
        {
            AudioSource audioSource = _audioSources[(int)Sound.Effect];
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(audioClip);
        }
    }

    public void Play(string name, Sound type = Sound.Effect, float pitch = 1.0f)
    {
        AudioClip audioClip = GetOrAddAudioClip(name, type);
        Play(audioClip, type, pitch);
    }
    AudioClip GetOrAddAudioClip(string name, Sound type = Sound.Effect)
    {
        AudioClip audioClip = null;

        if (type == Sound.Bgm) // BGM 배경음악 클립 붙이기
        {
            audioClip = Managers.Resource.GetAudio(name);
        }
        else // Effect 효과음 클립 붙이기
        {
            if (_audioClips.TryGetValue(name, out audioClip) == false)
            {
                audioClip = Managers.Resource.GetAudio(name);
                _audioClips.Add(name, audioClip);
            }
        }

        if (audioClip == null)
            Debug.Log($"AudioClip Missing ! {name}");

        return audioClip;
    }
}
