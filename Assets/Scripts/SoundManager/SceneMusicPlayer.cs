using UnityEngine;

public class SceneMusicPlayer : MonoBehaviour
{
    [SerializeField] private string musicID = "";
    [SerializeField] private bool loop = true;

    private void Start()
    {
        if (!string.IsNullOrEmpty(musicID))
            SoundManager.Instance.PlayMusic(musicID, loop);
    }
}
