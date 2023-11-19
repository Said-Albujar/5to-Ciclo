using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour, IDataPersistence
{
    public int health;
    public int maxHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public string SceneName;
    private bool once;

    private void Update()
    {
        if (health > maxHearts)
        {
            health = maxHearts;
        }

        // CheckHearts();
        if (health == 0)
        {
            if (!once)
            {
                AudioManager.Instance.PlaySFX("Dead");
            }
            DataPersistenceManager.instance.LoadGame();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            health -= 1;
        }
    }

    public void LoadData(GameData data)
    {
        this.health = data.health;
        this.maxHearts = data.maxHearts;
    }

    public void SaveData(ref GameData data)
    {
        data.health = this.health;
        data.maxHearts = this.maxHearts;
    }
}
