using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSounds : MonoBehaviour
{
    public PlayerMovement playerState;
    // Start is called before the first frame update
    public void FinichClimb()
    {
        playerState.FinishClimb();
    }
    public void StepSound()
    {
        AudioManager.Instance.PlaySFX("Walk");
    }
}
