using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMusicBoss : MonoBehaviour
{
    private bool hasPlayed = false; 

    private void OnTriggerEnter(Collider other)
    {
        if (!hasPlayed && other.gameObject.layer == LayerMask.NameToLayer("TargetPlayer"))
        {
            AudioManager.Instance.PlayMusic("Boss");
            hasPlayed = true; 
        }
    }
}
