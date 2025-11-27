using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
                CreateSingleton();

            return _instance;
        }
    }
    private static SoundManager _instance;


    private string currentMusicID = "";

    [Header("Audio Mixer")]
    public AudioMixer mixer;

    [Header("Mixer Parameters")]
    public string masterVolumeParam = "MasterVolume";
    public string musicVolumeParam = "MusicVolume";
    public string sfxVolumeParam = "SFXVolume";
    public string uiVolumeParam = "UIVolume";
    public string ambienceVolumeParam = "AmbienceVolume";
    public string continuousVolumeParam = "ContinuousVolume";

    [Header("Mixer Groups")]
    public AudioMixerGroup musicGroup;
    public AudioMixerGroup sfxGroup;
    public AudioMixerGroup uiGroup;
    public AudioMixerGroup ambienceGroup;
    public AudioMixerGroup continuousGroup;

    [Header("Audio Library")]
    public AudioLibrary library;

    private AudioSource musicSource;
    private AudioSource sfxSource;
    private AudioSource uiSource;
    private AudioSource ambienceSource;

    private AudioSource musicSourceA;
    private AudioSource musicSourceB;
    private bool isUsingA = true;

    private Dictionary<string, AudioSource> continuousSources = new Dictionary<string, AudioSource>();


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        SetupSources();
        LoadSavedVolumes();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }



    void SetupSources()
    {
        musicSourceA = CreateSource("Music_A", musicGroup);
        musicSourceB = CreateSource("Music_B", musicGroup);

        musicSourceA.loop = true;
        musicSourceB.loop = true;

        musicSourceA.volume = 0;
        musicSourceB.volume = 0;

        musicSource = musicSourceA;
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

    public void PlayContinuous(string id, float volume = 1f)
    {
        if (continuousSources.ContainsKey(id))
        {
            continuousSources[id].volume = volume;
            return;
        }

        AudioSource src = CreateSource("Continuous_" + id, continuousGroup);
        src.loop = true;

        AudioEvent evt = library.Get(id);
        if (evt == null) return;

        src.clip = evt.clip;
        src.volume = volume;
        src.pitch = evt.GetPitch();
        src.Play();

        continuousSources[id] = src;
    }

    public void StopContinuous(string id)
    {
        if (!continuousSources.ContainsKey(id)) return;

        AudioSource src = continuousSources[id];
        src.Stop();
        Destroy(src.gameObject);

        continuousSources.Remove(id);
    }

    public void FadeMusic(string id, float fadeTime = 1f)
    {
        if (currentMusicID == id)
            return;

        AudioEvent evt = library.Get(id);
        if (evt == null) return;

        currentMusicID = id;

        AudioSource newSource = isUsingA ? musicSourceB : musicSourceA;
        AudioSource oldSource = isUsingA ? musicSourceA : musicSourceB;

        isUsingA = !isUsingA;

        newSource.clip = evt.clip;
        newSource.volume = 0f;
        newSource.pitch = evt.GetPitch();
        newSource.loop = true;
        newSource.Play();

        StartCoroutine(FadeRoutine(oldSource, newSource, fadeTime));
    }

    private IEnumerator FadeRoutine(AudioSource oldSrc, AudioSource newSrc, float time)
    {
        float t = 0f;

        while (t < time)
        {
            t += Time.unscaledDeltaTime;
            float k = t / time;

            if (oldSrc != null)
                oldSrc.volume = Mathf.Lerp(1f, 0f, k);

            newSrc.volume = Mathf.Lerp(0f, 1f, k);

            yield return null;
        }

        if (oldSrc != null)
        {
            oldSrc.Stop();
            oldSrc.volume = 0;
        }

        newSrc.volume = 1f;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SoundManager.Instance.FadeMusic("bgm_cannon", 1f);
    }

    private static void CreateSingleton()
    {
        SoundManager existing = FindObjectOfType<SoundManager>();
        if (existing != null)
        {
            _instance = existing;
            return;
        }

        SoundManager prefab = Resources.Load<SoundManager>("SoundManager");
        if (prefab != null)
        {
            _instance = Instantiate(prefab);
            _instance.gameObject.name = "SoundManager (AutoCreated)";
            return;
        }

        Debug.LogError("SoundManager prefab not found in Resources folder!");
    }


}
