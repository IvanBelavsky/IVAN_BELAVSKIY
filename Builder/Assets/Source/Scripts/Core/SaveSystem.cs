using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static void SaveBuildings(List<Building> buildings)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/buildings.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        List<BuildingSaveData> saveDataList = new List<BuildingSaveData>();

        foreach (var building in buildings)
        {
            saveDataList.Add(new BuildingSaveData(building));
        }

        binaryFormatter.Serialize(stream, saveDataList);
        stream.Close();

        Debug.Log(path);
    }

    public static List<BuildingSaveData> LoadBuildings()
    {
        string path = Application.persistentDataPath + "/buildings.fun";
        if (File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            List<BuildingSaveData> data = binaryFormatter.Deserialize(stream) as List<BuildingSaveData>;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}