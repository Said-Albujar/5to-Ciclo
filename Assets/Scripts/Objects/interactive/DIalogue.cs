using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DIalogue: MonoBehaviour
{
    public GameObject dialogueIcon;
    public GameObject dialoguePanelText;
    [SerializeField, TextArea(4, 6)] private string[] dialogueTextBox;
    public TextMeshProUGUI textDialogue;
    public bool dialogueExist;
    public bool dialogueStart;
    int index;

    public bool HavePass;
    public GameObject PassText;
    private bool dialogueIconDeactivated;

    void Start()
    {
    }

    void Update()
    {
        if (dialogueExist && Input.GetKeyDown(KeyCode.E))
        {
            AudioManager.Instance.PlaySFX("Dialogue");
            if (!dialogueStart)
            {
                StartDialogue();
            }
            else if (textDialogue.text == dialogueTextBox[index])
            {
                NextDialogue();
            }
            else
            {
                StopAllCoroutines();
                textDialogue.text = dialogueTextBox[index];
            }
        }
    }

    private IEnumerator CountLineText()
    {
        textDialogue.text = string.Empty;
        foreach (char character in dialogueTextBox[index])
        {
            textDialogue.text += character;
            yield return new WaitForSeconds(0.02f);
        }
    }

    void StartDialogue()
    {
        dialogueStart = true;
        dialoguePanelText.SetActive(true);

        if (!dialogueIconDeactivated)
        {
            dialogueIcon.SetActive(false);
            dialogueIconDeactivated = true;
        }

        index = 0;
        StartCoroutine(CountLineText());

        if (HavePass)
        {
            PassText.SetActive(true);
        }
    }

    private void NextDialogue()
    {
        index++;
        if (index < dialogueTextBox.Length)
        {
            StartCoroutine(CountLineText());
        }
        else
        {
            dialogueStart = false;
            dialoguePanelText.SetActive(false);
            if (!dialogueIconDeactivated)
            {
                dialogueIcon.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("TargetPlayer"))
        {
            dialogueExist = true;
            if (!dialogueIconDeactivated)
            {
                dialogueIcon.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("TargetPlayer"))
        {
            dialogueStart = false;
            index = 0;
            dialogueExist = false;
            if (!dialogueIconDeactivated)
            {
                dialogueIcon.SetActive(true);
            }
            dialoguePanelText.SetActive(false);
        }
    }
}
