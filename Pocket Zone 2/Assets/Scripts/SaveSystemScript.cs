using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using AYellowpaper;
using AYellowpaper.SerializedCollections;



[Serializable]
public class Slot
{
    public string ItemName;
    public int Count;
}

[Serializable]
public class Character
{
    public Vector2 Position;
    public float Health;
    public List<Slot> Slots = new List<Slot>(15);
}

public class SaveSystemScript : MonoBehaviour
{
    public CharacterScript Character;
    public Character SaveChar = new Character();

    string SavePath;

    public SerializedDictionary<string, ItemSO> Items = new SerializedDictionary<string, ItemSO>();


    void Start ()
    {
        print(Application.persistentDataPath);
        SavePath = Application.persistentDataPath + "/Save.sv";
        
        if(!File.Exists(SavePath))
        {
            Save();
        }
        else
        {
            Load();
        }

        InvokeRepeating("Save", 5, 5);
    }



    public void Save ()
    {
        print("Save!");

        SaveChar.Health = Character._Health.CurrentHealth;
        SaveChar.Position = Character.transform.position;

        for (int i = 0; i < Character.Slots.Count; i++)
        {
            if(Character.Slots[i].Item == null)
            {
                SaveChar.Slots[i].ItemName = string.Empty;
                SaveChar.Slots[i].Count = 0;
            }
            else
            {
                SaveChar.Slots[i].ItemName = Character.Slots[i].Item.Name;
                SaveChar.Slots[i].Count = Character.Slots[i].Count;
            }
        }

        string json = JsonUtility.ToJson(SaveChar);

        File.WriteAllText(SavePath, json);
    }

    public void Load ()
    {
        string fileContents = File.ReadAllText(SavePath);
        SaveChar = JsonUtility.FromJson<Character>(fileContents);

        Character._Health.SetHealth(SaveChar.Health);
        Character.transform.position = SaveChar.Position;

        for (int i = 0; i < Character.Slots.Count; i++)
        {
            if(SaveChar.Slots[i].ItemName != "")
                Character.Slots[i].SetItemSlot(Items[SaveChar.Slots[i].ItemName], SaveChar.Slots[i].Count);
        }
    }
}
