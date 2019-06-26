using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerController : MonoBehaviour
{
    public Player player;
    void Start()
    {
        player = new Player();
        player.DirectoryName();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            player.Save();
            print("Create file in " + player.GetPath());
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.Load();
            print("Load file in " + player.GetPath());
        }
    }
}

[Serializable]
public class Player
{
    private string path = "";
    public string name = "";
    [SerializeField] private int power = 0;
    [SerializeField] private float life = 0;

    public List<int> weapons = new List<int>();
    
    public void Save()
    {
        var content = JsonUtility.ToJson(this, true);
        File.WriteAllText(path, content);
    }
    public void Load()
    {
        var content = File.ReadAllText(path);
        var jsonData = JsonUtility.FromJson<Player>(content);

        name = jsonData.name;
        power = jsonData.power;
        life = jsonData.life;

        weapons = jsonData.weapons;
    }
    public string GetPath()
    {
        return path;
    }
    public void DirectoryName()
    {
        if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/JsonSaveAndLoad"))
        {
            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/JsonSaveAndLoad");
        }
        path = string.Format(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/JsonSaveAndLoad/config.json");
    }
}
