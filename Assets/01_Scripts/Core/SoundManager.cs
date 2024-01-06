using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum SoundEnum
{
    EFFECT,
    BGM,
    END
}

public class SoundManager : MonoSingleton<SoundManager>
{
    [SerializeField] private AudioClipSO _audioClipSO;
    [SerializeField] private AudioClipSO _bgmClipSO;
    private AudioSource _audioSource;

    [SerializeField] private AudioMixer _masterMixer;
    [SerializeField] private AudioMixerGroup _bgmGroup;
    [SerializeField] private AudioMixerGroup _sfxGroup;

    public float soundFadeOnTime;

    private AudioSource[] _audioSources = new AudioSource[(int)SoundEnum.END];

    private void Awake()
    {
        Init();
    }

    public override void Init()
    {
        string[] soundNames = System.Enum.GetNames(typeof(SoundEnum));
        for (int i = 0; i < soundNames.Length - 1; i++)
        {
            GameObject go = new GameObject { name = soundNames[i] };
            _audioSources[i] = go.AddComponent<AudioSource>();
            _audioSources[i].playOnAwake = false;
            _audioSources[i].outputAudioMixerGroup = (soundNames[i] == "BGM" ? _bgmGroup : _sfxGroup);
            go.transform.SetParent(transform);
        }

        _audioSources[(int)SoundEnum.BGM].loop = true;

        PlayBGM("TitleBGM");
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlaySFX("Btn1");
        }
    }
    public void PlaySFX(string clipName)
    {
        AudioClip clip = _audioClipSO.GetAudioClip(clipName);
        Play(clip, SoundEnum.EFFECT);
    }

    public void PlayBGM(string clipName)
    {
        AudioClip clip = _bgmClipSO.GetAudioClip(clipName);
        Play(clip, SoundEnum.BGM);
    }

    public void Play(AudioClip audioClips, SoundEnum type = SoundEnum.EFFECT)
    {
        if (audioClips == null)
        {
            Debug.LogError("cannot find audioclips");
            return;
        }

        if (type == SoundEnum.BGM)
        {
            StopAllCoroutines();
            AudioSource audioSource = _audioSources[(int)SoundEnum.BGM];

            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.volume = 0;
            audioSource.clip = audioClips;
            audioSource.Play();

            StartCoroutine(SoundFade(true, _audioSources[(int)SoundEnum.BGM], soundFadeOnTime, 1, SoundEnum.BGM));
            StartCoroutine(SoundFade(false, _audioSources[(int)SoundEnum.BGM], soundFadeOnTime, 0, SoundEnum.BGM));
        }
        else
        {
            AudioSource audioSource = _audioSources[(int)SoundEnum.EFFECT];
            audioSource.PlayOneShot(audioClips);
        }
    }

    public void Stop()
    {
        foreach (var audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
    }

    public void Mute(SoundEnum type, bool mute)
    {
        _masterMixer.SetFloat(type.ToString().ToLower(), mute ? -80 : 0);
    }
    IEnumerator SoundFade(bool fadeIn, AudioSource source, float duration, float endVolume, SoundEnum type)
    {
        if (!fadeIn)
        {
            yield return new WaitForSeconds((float)(source.clip.length - duration));
        }

        float time = 0f;
        float startVolume = source.volume;

        while (time < duration)
        {
            time += Time.deltaTime;
            source.volume = Mathf.Lerp(startVolume, endVolume, time / duration);
            yield return null;
        }

        if (!fadeIn)
            Play(source.clip, type);
    }


}
