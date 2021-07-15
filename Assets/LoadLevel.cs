using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevel : MonoBehaviour
{
    GameManager instance;

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = GameManager.instance;
        }

        if(instance.player.enemies.Count <= 0)
        {
            instance.player.enemies = instance.GetEnemies();
        }
        
        if(instance.player.enemies.Count > 0)
        {
            List<GameObject> enemiesToDestroy = new List<GameObject>();
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                if (!instance.player.enemies.Contains(enemy.name))
                {
                    enemiesToDestroy.Add(enemy);
                }
            }
            foreach (GameObject gone in enemiesToDestroy)
            {
                Destroy(gone);
            }
            enemiesToDestroy.Clear();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveGame()
    {
        instance.SaveGame();
    }
}
