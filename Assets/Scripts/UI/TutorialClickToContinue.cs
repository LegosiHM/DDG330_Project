using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialClickToContinue : MonoBehaviour
{
    [Header("Next Scene")]
    public string nextSceneName;

    void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
