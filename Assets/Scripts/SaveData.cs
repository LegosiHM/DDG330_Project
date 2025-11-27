using UnityEngine;

public class SaveData : MonoBehaviour
{
    private static SaveData _instance;
    private static bool _initialized = false;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void AutoCreate()
    {
        _instance = FindObjectOfType<SaveData>();

        if (_instance == null)
        {
            GameObject obj = new GameObject("SaveData");
            _instance = obj.AddComponent<SaveData>();
        }

        DontDestroyOnLoad(_instance.gameObject);
    }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        if (_initialized == false)
        {
            var config = new FBPPConfig()
            {
                SaveFileName = "saveData.txt",
                AutoSaveData = true,
                ScrambleSaveData = false,
                SaveFilePath = Application.persistentDataPath
            };

            FBPP.Start(config);
            _initialized = true;
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            FBPP.DeleteAll();
            FBPP.Save();

            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();

            Debug.Log("ALL SAVE DATA DELETED (FBPP + PlayerPrefs)");
        }
    }
}
