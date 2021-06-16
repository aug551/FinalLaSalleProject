using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
    public string username;
    public string name;

    public static Player CreateFromJSON(string json)
    {
        return JsonUtility.FromJson<Player>(json);
    }

}
