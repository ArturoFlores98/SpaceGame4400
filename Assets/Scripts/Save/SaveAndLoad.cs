using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveAndLoad
{

    private static string filename = "SaveData.txt";
    private static string directoryName = "SaveData";

    public static void SaveState(SaveObject so)
    {
        if(!DirectoryExists())
            Directory.CreateDirectory(Application.persistentDataPath + "/" + directoryName);
        
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(GetSavePath());
            bf.Serialize(file, so);
            file.Close();
        
    }

    public static SaveObject LoadState()
    {
        SaveObject so = new SaveObject();

        if (SaveExists())
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(GetSavePath(), FileMode.Open);
                so = (SaveObject)bf.Deserialize(file);
                file.Close();
            }
            catch(SerializationException)
            {
                Debug.LogWarning("failed to load save");
            }

        }
        else
        {
            SaveState(so);
        }
        return so;
    }

    private static bool SaveExists()
    {
        return File.Exists(GetSavePath());
    }

    private static bool DirectoryExists()
    {
        return Directory.Exists(Application.persistentDataPath + "/" + directoryName);
    }

    private static string GetSavePath()
    {
        return Application.persistentDataPath + "/" + directoryName + "/" + filename;
    }
}
