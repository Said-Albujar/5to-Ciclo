using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventController : MonoBehaviour
{
   // public EnemyAudioManager enemyAudioManager;
    public EnemyThrower enemyThrower;
    public void PlayStepSound()
    {
        //enemyAudioManager.PlaySound("Steps");
    }
    public void ShootEvent()
    {
        enemyThrower.Shoot();
    }
    
    
    
    
}
