using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectButton : MonoBehaviour
{
    [Header("Level Settings")]
    public string levelID;              
    public string tutorialSceneName;     

    [Header("UI References")]
    public Button button;
    public GameObject lockIcon;

    [Header("Stars")]
    public Image[] starSlots;
    public Sprite filledStar;
    public Sprite emptyStar;

    private bool isUnlocked = false;

    void Start()
    {
        SetupButtonState();
        SetupStarDisplay();
    }

    void SetupButtonState()
    {
        if (levelID == "Level1")
        {
            isUnlocked = true; // ALWAYS unlocked
        }
        else
        {
            isUnlocked = FBPP.GetInt(levelID + "_Unlocked", 0) == 1;
        }

        lockIcon.SetActive(!isUnlocked);
        button.interactable = isUnlocked;
    }


    void SetupStarDisplay()
    {
        int bestStars = FBPP.GetInt(levelID + "_BestStars", 0);

        for (int i = 0; i < starSlots.Length; i++)
        {
            starSlots[i].sprite = (i < bestStars ? filledStar : emptyStar);
        }
    }

    public void OnLevelButtonPressed()
    {
        if (!isUnlocked)
            return;

        SceneManager.LoadScene(tutorialSceneName);
    }
}
