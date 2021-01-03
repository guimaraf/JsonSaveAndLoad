using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerController : MonoBehaviour
{
    public Player player;
    void Start()
    {
        // criando o objeto do pplayer
        player = new Player();

        // verificando se existe a pasta e o arquivo
        // após encontrar o arquivo, ele será carregado no script
        // o próprio método que verifica se existe, já cria a pasta e arquivos necessários
        if (player.DirectoryExists())
        {
            print("Arquivo carregado");
            player.Load();
        }
        
        else
        {
            print("Arquivo não encontrado, criando diretório e arquivo de configuração");
        }
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

    public List<string> weapons = new List<string>(2);
    
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

    public bool DirectoryExists()
    {
        bool existFile = false;
        
        // aqui é onde a mágica acontece, verifico se existe a pasta, se não existir, será criado a pasta e também o arquivo de configuração, chamando o
        // método Save()
        // se o arquivo já existir, vai apenas retornar verdadeiro e feito o load das informações

        if(!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/JsonSaveAndLoad"))
        {
            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/JsonSaveAndLoad");
            path = string.Format(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/JsonSaveAndLoad/config.json");
            Save();
        }
        else
        {
            existFile = true;
        }
        return existFile;
    }
}
