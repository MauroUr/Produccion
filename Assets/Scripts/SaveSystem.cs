using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveData(OptionsData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/optionsData";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static OptionsData LoadOptions()
    {
        string path = Application.persistentDataPath + "/optionsData";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            OptionsData data = new OptionsData();

            if (stream.Length > 0)
                data = formatter.Deserialize(stream) as OptionsData;
            stream.Close();

            return data;
        }
        else
            return null;
    }
    public static void SaveProgress(int progress)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/progressData";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, progress);
        stream.Close();
    }
    public static int LoadProgress()
    {
        string path = Application.persistentDataPath + "/progressData";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            int data = (int)formatter.Deserialize(stream);
            stream.Close();

            return data;
        }
        else
            return 0;
    }

    
}
