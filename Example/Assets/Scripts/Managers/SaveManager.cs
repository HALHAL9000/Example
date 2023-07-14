using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    private int money;
    private int level;
    private int clearlevel;
    private int income;
    private int firerate;
    private int damage;


    public int Money { get { return money; } set { money = value; } }
    public int Level { get { return level; } set { level = value; } }
    public int ClearLevel { get { return clearlevel; } set { clearlevel = value; } }
    public int Income { get { return income; } set { income = value; } }

    public int FireRate { get { return firerate; } set { firerate = value; } }
    public int Damage { get { return damage; } set { damage = value; } }

    private void Awake()
    {
        LoadFromJson();

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SaveToJson()
    {
        SaveData data = new SaveData();
        data.Level = level;
        data.Money = money;
        data.ClearLevel = clearlevel;
        data.Income = income;
        data.FireRate = firerate;
        data.Damage = damage;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(Application.persistentDataPath + ("/savefile.json"), json);
    }

    public void LoadFromJson()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            level = data.Level;
            money = data.Money;
            clearlevel = data.ClearLevel;
            income = data.Income;
            firerate = data.FireRate;
            damage = data.Damage;
        }
        else
        {
            level = 1;
            money = 300;
            clearlevel = 1;
            income = 1;
            damage = 1;
            firerate = 1;
            SaveToJson();
        }
    }
}
