using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public static class SaveSystem
{
    public static void SaveScene(Scene scene)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/scena.diocane";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, scene);
        stream.Close();
    }

    public static Scene LoadScene()
    {
        string path = Application.persistentDataPath + "/scena.diocane";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            Scene scene = formatter.Deserialize(stream) as Scene;

            return scene;
        }
        else
        {
            return null;    
        }

    }

}
