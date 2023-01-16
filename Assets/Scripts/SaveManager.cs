using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager
{
    public Map map;

    public static void Save(Map map)
    {
        var binaryFormatter = new BinaryFormatter();
        var mapFile = File.Open(Application.persistentDataPath + "/save.dat", FileMode.OpenOrCreate);
        
        binaryFormatter.Serialize(mapFile, map);
        mapFile.Close();
    }
    
    public static Map Load()
    {
       var binaryFormatter = new BinaryFormatter();
       var filePath = Application.persistentDataPath + "/save.dat";
       if (File.Exists(filePath))
       {
           var mapFile = File.Open(filePath, FileMode.OpenOrCreate);
        
           var mapObject = (Map)binaryFormatter.Deserialize(mapFile);
           mapFile.Close();
           return mapObject;
       }

       return null;
    }

    public static bool HasSavedMap()
    {
        var filePath = Application.persistentDataPath + "/save.dat";
        return File.Exists(filePath);
    }
}
