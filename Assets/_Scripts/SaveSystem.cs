using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void Save(GameData data)
    {
        BinaryFormatter form = new BinaryFormatter();
        FileStream fs = new FileStream(Path(), FileMode.Create);
        form.Serialize(fs, data);
        fs.Close();
    }

    public static GameData LoadData()
    {
        if (!File.Exists(Path()))
        {
            GameData newDat = new GameData();
            return newDat;
        }
        BinaryFormatter form = new BinaryFormatter();
        FileStream fs = new FileStream(Path(), FileMode.Open);
        GameData data = form.Deserialize(fs) as GameData;
        fs.Close();

        return data;
    }

    public static string Path()
    {
        return Application.persistentDataPath + "/Data.txt";
    }
}
