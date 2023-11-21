using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hair") || other.CompareTag("Miner") || other.CompareTag("Engi"))
            {
            AudioManager.Instance.PlaySFX("Checkpoint");
            DataPersistenceManager.instance.SaveGame();
            Destroy(this.gameObject);
        }
        
    }
}
