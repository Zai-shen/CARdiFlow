using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    // Editor: C:\Users\USERNAME\AppData\LocalLow\DefaultCompany\OPlan\AppSaves
    private static readonly string SAVE_FOLDER = Application.persistentDataPath + "/AppSaves";
    private const string FILEFORMAT_STANDARD = "save";
    private const string FILEFORMAT_JSON = "json";


    private static void Init()
    {
        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }
    }

    public static void SaveJSON<T>(T objectToSave, string fileName)
    {
        Init();

        string jsonRepresentation = JsonUtility.ToJson(objectToSave, true);
        File.WriteAllText($"{SAVE_FOLDER}/{fileName}.{FILEFORMAT_JSON}", jsonRepresentation);
    }

    public static T LoadJSON<T>(string fileName)
    {
        if (File.Exists($"{SAVE_FOLDER}/{fileName}.{FILEFORMAT_JSON}"))
        {
            string jsonRep = File.ReadAllText($"{SAVE_FOLDER}/{fileName}.{FILEFORMAT_JSON}");
            
            return JsonUtility.FromJson<T>(jsonRep);
        }
        else
        {
            Debug.LogError($"Save file not found in {SAVE_FOLDER}/{fileName}.{FILEFORMAT_JSON}");
            return default(T);
        }
    }

    public static void SaveBinary<T>(T objectToSave, string fileName, string optFormat = FILEFORMAT_STANDARD)
    {
        Init();

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream($"{SAVE_FOLDER}/{fileName}.{optFormat}", FileMode.Create);

        formatter.Serialize(stream, objectToSave);
        stream.Close();
    }

    public static T LoadBinary<T>(string fileName, string optFormat = FILEFORMAT_STANDARD)
    {
        if (File.Exists($"{SAVE_FOLDER}/{fileName}.{optFormat}"))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream($"{SAVE_FOLDER}/{fileName}.{optFormat}", FileMode.Open);

            T tLoaded = (T)formatter.Deserialize(stream);
            stream.Close();

            return tLoaded;
        }
        else
        {
            Debug.LogError($"Save file not found in {SAVE_FOLDER}/{fileName}.{optFormat}");
            return default(T);
        }
    }

}
