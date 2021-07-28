using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevel : MonoBehaviour
{
    GameManager instance;
    GameObject[] enemiesInLevel;
    [SerializeField] private GameObject nextLevel;

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

        if (instance.player.position != Vector3.zero || instance.player.position != null)
        {
            GameObject.FindGameObjectWithTag("Player").transform.position = instance.player.position;
        }

    }

    // Update is called once per frame
    void Update()
    {
        enemiesInLevel = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemiesInLevel.Length <= 0)
        {
            Debug.Log("Empty");
            nextLevel.SetActive(true);
        }
    }

    public void SaveGame()
    {
        instance.SaveGame();
    }
}
