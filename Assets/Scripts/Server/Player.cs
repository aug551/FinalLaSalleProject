using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
    public string username;
    public string name;
    public string password;
    public int level;
    public float hp;

    public Player(string username, string name, string password)
    {
        this.username = username;
        this.name = name;
        this.password = password;
    }

    public static Player CreateFromJSON(string json)
    {
        return JsonUtility.FromJson<Player>(json);
    }

}
