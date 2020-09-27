using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void savePlayer(Player[] player, string savedName)
    {
        //Formatting
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player."+savedName; //gets a path to a data directry on the OS that won't change
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);

        //Insert into a file

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer(string savedName)
    {

        try
        {
            for (int i = 0; i < 10; i++)
            {
                PlayerPrefs.GetString(i.ToString());
            }
        }
        catch
        {

        }
        
      //  string path = Application.persistentDataPath + "/player.savestate";
        string path = Application.persistentDataPath + "/player."+savedName;
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in: " + path);
            return null;
        }
    }
   
}
