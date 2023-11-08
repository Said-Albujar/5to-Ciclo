using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeCrouch : MonoBehaviour
{
    public TMP_Text textButtonCrouch;
    private string typeCrouch;
    // Start is called before the first frame update
    void Start()
    {
        UpdateTypeCrouchText();
        
    }

    private void UpdateTypeCrouchText()
    {
        switch (GameManager.instance.playerMovement.hold)
        {
            case true:
                typeCrouch = "Presionar";
                break;
            default:
                typeCrouch = "Mantener";
                break;
        }
        textButtonCrouch.text = typeCrouch;
    }
    public void HoldPress()
    {
        switch (GameManager.instance.playerMovement.hold)
        {
            case true:
                GameManager.instance.playerMovement.hold = false;
                break;
            default:
                GameManager.instance.playerMovement.hold = true;
                break;
        }
        UpdateTypeCrouchText();
    }
}
