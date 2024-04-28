using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int maxHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public string SceneName;
    private bool once;
    public GameObject panelDead;
    public float timer, maxTimer;
    public Animator anim;
    public PlayerMovement player;
    private void Update()
    {

        if (health == 0)
        {
           
            anim.Play("dead");
            //anim.SetBool("dead", true);
            panelDead.SetActive(true);
            player.GetComponent<PlayerMovement>().enabled = false;
            timer += Time.deltaTime;
            if (!once)
            {
                AudioManager.Instance.PlaySFX("Dead");
                once = true;
            }
            if(timer>=maxTimer)
            {
                health = maxHearts;
                DataPersistenceManager.instance.LoadGame();
                timer = 0f;
                player.currentstate = PlayerMovement.state.idle;
                
            }
        }
        else
        {
            anim.SetBool("dead", false);
            //anim.SetBool("dead", true);

            player.GetComponent<PlayerMovement>().enabled = true;

            panelDead.SetActive(false);
            once = false;

        
        }
       
    }
   /* public void DataManager()
    {
        DataPersistenceManager.instance.LoadGame();

    }*/

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            health -= 1;
            if(health<=0)
            {
                health = 0;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            health -= 1;
            if (health <= 0)
            {
                health = 0;
            }
        }
    }


}
