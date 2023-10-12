using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public List<GameObject> enemiesPrefabs = new List<GameObject>();
    [SerializeField] private bool active = false;
    private bool once;

    // Update is called once per frame
    void Update()
    {
        if (!once && active) //se usa once para que se cumpla una sola vez
        {
            foreach (GameObject enemy in enemiesPrefabs)
            {
                enemy.SetActive(true);
            }
            once = true;
        }
    }
}
