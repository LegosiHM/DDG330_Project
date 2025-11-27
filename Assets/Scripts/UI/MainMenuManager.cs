using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject optionPanel;
    [SerializeField] private GameObject creditPanel;
    [SerializeField] private GameObject[] creditPages;

    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;
    private int currentPage = 0;

    public void Start()
    {
        SoundManager.Instance.FadeMusic("bgm_cannon", 1f);
        float saved = SoundManager.Instance.GetVolume("MasterVolume");
        musicSlider.value = saved;
        SetMusicVolume();
        creditPanel.SetActive(false);
        optionPanel.SetActive(false);
    }
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void ToggleOption()
    {
        optionPanel.SetActive(!optionPanel.activeSelf);
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;

        
        if (volume < 0.0001f)
            volume = 0.0001f;

        myMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }

    public void OpenCredit()
    {
        creditPanel.SetActive(true);
        ShowCreditPage(0);
    }

    public void NextCreditPage()
    {
        if (currentPage + 1 < creditPages.Length)
        {
            ShowCreditPage(currentPage + 1);
        }
    }

    public void CloseCredit()
    {
        creditPanel.SetActive(false);
    }

    void ShowCreditPage(int index)
    {
        for (int i = 0; i < creditPages.Length; i++)
        {
            creditPages[i].SetActive(i == index);
        }
        currentPage = index;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void MainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
}