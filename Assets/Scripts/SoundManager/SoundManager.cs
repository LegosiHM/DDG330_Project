using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    private string currentMusicID = "";

    [Header("Audio Mixer")]
    public AudioMixer mixer;

    [Header("Mixer Parameters")]
    public string masterVolumeParam = "MasterVolume";
    public string musicVolumeParam = "MusicVolume";
    public string sfxVolumeParam = "SFXVolume";
    public string uiVolumeParam = "UIVolume";
    public string ambienceVolumeParam = "AmbienceVolume";

    [Header("Mixer Groups")]
    public AudioMixerGroup musicGroup;
    public AudioMixerGroup sfxGroup;
    public AudioMixerGroup uiGroup;
    public AudioMixerGroup ambienceGroup;

    [Header("Audio Library")]
    public AudioLibrary library;

    private AudioSource musicSource;
    private AudioSource sfxSource;
    private AudioSource uiSource;
    private AudioSource ambienceSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        SetupSources();
        LoadSavedVolumes();
    }

    void SetupSources()
    {
        musicSource = CreateSource("MusicSource", musicGroup);
        sfxSource = CreateSource("SFXSource", sfxGroup);
        uiSource = CreateSource("UISource", uiGroup);
        ambienceSource = CreateSource("AmbienceSource", ambienceGroup);
    }

    AudioSource CreateSource(string name, AudioMixerGroup group)
    {
        GameObject obj = new GameObject(name);
        obj.transform.parent = transform;

        AudioSource src = obj.AddComponent<AudioSource>();
        src.playOnAwake = false;
        src.loop = false;
        src.outputAudioMixerGroup = group;

        return src;
    }

    public void PlaySFX(string id)
    {
        PlayOneShot(id, sfxSource);
    }

    public void PlayUI(string id)
    {
        PlayOneShot(id, uiSource);
    }

    public void PlayAmbience(string id, bool loop = true)
    {
        PlayLoop(id, ambienceSource, loop);
    }

    public void PlayMusic(string id, bool loop = true)
    {
        if (currentMusicID == id && musicSource.isPlaying)
            return;

        AudioEvent evt = library.Get(id);
        if (evt == null) return;

        currentMusicID = id;

        musicSource.clip = evt.clip;
        musicSource.volume = evt.volume;
        musicSource.pitch = evt.GetPitch();
        musicSource.loop = loop;
        musicSource.Play();
    }


    void PlayOneShot(string id, AudioSource source)
    {
        AudioEvent evt = library.Get(id);
        if (evt == null) return;

        source.pitch = evt.GetPitch();
        source.PlayOneShot(evt.clip, evt.volume);
    }

    void PlayLoop(string id, AudioSource source, bool loop)
    {
        AudioEvent evt = library.Get(id);
        if (evt == null) return;

        source.clip = evt.clip;
        source.pitch = evt.GetPitch();
        source.volume = evt.volume;
        source.loop = loop;
        source.Play();
    }


    public void SetVolume(string mixerParam, float value)
    {
        value = Mathf.Clamp(value, 0.0001f, 1f);
        mixer.SetFloat(mixerParam, Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat(mixerParam, value);
    }

    public float GetVolume(string mixerParam, float defaultValue = 0.75f)
    {
        return PlayerPrefs.GetFloat(mixerParam, defaultValue);
    }

    void LoadSavedVolumes()
    {
        SetVolume(masterVolumeParam, GetVolume(masterVolumeParam));
        SetVolume(musicVolumeParam, GetVolume(musicVolumeParam));
        SetVolume(sfxVolumeParam, GetVolume(sfxVolumeParam));
        SetVolume(uiVolumeParam, GetVolume(uiVolumeParam));
        SetVolume(ambienceVolumeParam, GetVolume(ambienceVolumeParam));
    }
}
