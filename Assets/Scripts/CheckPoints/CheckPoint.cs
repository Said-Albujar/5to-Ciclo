using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] int numCheckpoint;
    [SerializeField] PlayerMovement player;
    public Animator anim;

    void Start()
    {
        player = Transform.FindAnyObjectByType<PlayerMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hair") || other.CompareTag("Miner") || other.CompareTag("Engi"))
        {
            anim.Play("Checkpoint");
            if(player != null)
            {
                player.numCheckpoint = numCheckpoint;
            }

            AudioManager.Instance.PlaySFX("Checkpoint");
            DataPersistenceManager.instance.SaveGame();
            Destroy(this.gameObject);
        }
        
    }
}
