using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ResultScreenManager : MonoBehaviour
{
    public static ResultScreenManager Instance;

    [Header("UI Root Panel")]
    public GameObject panel;

    [Header("Stars UI")]
    public Image[] starSlots;
    public Sprite filledStar;
    public Sprite emptyStar;

    [Header("Time UI")]
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI bestTimeText;
    public Color highlightColor = Color.yellow;

    [Header("Buttons")]
    public Button nextLevelButton;
    public Button retryButton;
    public Button mainMenuButton;
    

    [Header("Scene Navigation")]
    public string nextLevelSceneName;

    private float _startTime;
    private string _levelName;

    void Awake()
    {
        Instance = this;
        _levelName = SceneManager.GetActiveScene().name;
    }

    void Start()
    {
        _startTime = Time.time;

        if (panel != null)
            panel.SetActive(false);
    }

    public void ShowResult()
    {
        panel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        UpdateStars();
        UpdateTimes();

        UnlockNextLevel();

        SetupButtons();
    }

    private void UpdateStars()
    {
        int collected = StarManager.Instance.GetCurrentStars();
        int previousBest = FBPP.GetInt(_levelName + "_BestStars", 0);

        for (int i = 0; i < starSlots.Length; i++)
            starSlots[i].sprite = (i < collected ? filledStar : emptyStar);

        if (collected > previousBest)
        {
            FBPP.SetInt(_levelName + "_BestStars", collected);
            FBPP.Save();
        }
    }

    private void UpdateTimes()
    {
        float time = Time.time - _startTime;

        timeText.text = "Time: " + time.ToString("F2") + " sec";

        float best = FBPP.GetFloat(_levelName + "_BestTime", Mathf.Infinity);

        if (time < best)
        {
            bestTimeText.text = "Best: " + time.ToString("F2") + " sec";
            bestTimeText.color = highlightColor;
            FBPP.SetFloat(_levelName + "_BestTime", time);
            FBPP.Save();
        }
        else
        {
            bestTimeText.text = "Best: " + best.ToString("F2") + " sec";
        }
    }



    private void SetupButtons()
    {
        if(nextLevelSceneName != null && nextLevelButton != null)
        {
            nextLevelButton.gameObject.SetActive(!string.IsNullOrEmpty(nextLevelSceneName));
        }
    }

    public void LoadNextLevel()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (_levelName == "Level-1-Final") FBPP.SetInt("Level2_Unlocked", 1);
        if (_levelName == "Level-2-Final") FBPP.SetInt("Level3_Unlocked", 1);
        if (_levelName == "Level-3-Final") FBPP.SetInt("Level4_Unlocked", 1);

        FBPP.Save();

        if (!string.IsNullOrEmpty(nextLevelSceneName))
            SceneManager.LoadScene(nextLevelSceneName);
    }



    public void RetryLevel()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMainMenu()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SceneManager.LoadScene("MainMenu");
    }

    private void UnlockNextLevel()
    {
        if (_levelName == "Level-1-Final") FBPP.SetInt("Level2_Unlocked", 1);
        if (_levelName == "Level-2-Final") FBPP.SetInt("Level3_Unlocked", 1);
        if (_levelName == "Level-3-Final") FBPP.SetInt("Level4_Unlocked", 1);

        FBPP.Save();
    }
}
