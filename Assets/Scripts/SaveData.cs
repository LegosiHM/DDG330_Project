using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{

    void Start()
    {
        var config = new FBPPConfig();
        {
            config.SaveFileName = "saveData.txt";
            config.AutoSaveData = true;
            config.ScrambleSaveData = false;
            config.SaveFilePath = Application.persistentDataPath;
        }

        FBPP.Start(config);

        Debug.Log("Save Path: " + Application.persistentDataPath);
    }
}