using UnityEngine;
using TMPro;

public class StarManager : MonoBehaviour
{
    public static StarManager Instance;

    [Header("UI")]
    public TextMeshProUGUI starText;

    [Header("Level Info")]
    public string levelName;    
    public int totalStars = 3;   

    private int starsCollected = 0;

    void Awake()
    {
        // make global instance
        Instance = this;
    }

    void Start()
    {
        ResetLevelStars();
        UpdateStarUI();
    }

    public void CollectStar()
    {
        starsCollected++;
        UpdateStarUI();
    }

    public void SaveBestStars()
    {
        int previousBest = FBPP.GetInt(levelName + "_BestStars", 0);

        if (starsCollected > previousBest)
        {
            FBPP.SetInt(levelName + "_BestStars", starsCollected);
            FBPP.Save();
        }
    }

    public void ResetLevelStars()
    {
        starsCollected = 0;
    }

    private void UpdateStarUI()
    {
        if (starText != null)
            starText.text = starsCollected + " / " + totalStars;
    }

    public int GetCurrentStars()
    {
        return starsCollected;
    }
}
