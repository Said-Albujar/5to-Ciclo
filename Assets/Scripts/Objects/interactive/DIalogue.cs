using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DIalogue : MonoBehaviour
{
    public GameObject dialogueIcon;
    public GameObject dialoguePanelText;
    [SerializeField,TextArea(4,6)] private string[] dialogueTextBox;
    public TextMeshProUGUI textDialogue;
    public bool dialogueExist;
    public bool dialogueStart;
    int index;

    public bool HavePass;
    public GameObject PassText;
    // Start is called before the first frame update
    void Start()
    {
       // dialogueIcon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(dialogueExist&&Input.GetKeyDown(KeyCode.E))
        {
            if(!dialogueStart)
            {
                StartDialogue();

            }
            else if(textDialogue.text==dialogueTextBox[index])
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
        foreach(char character in dialogueTextBox[index])
        {
            textDialogue.text += character;
            yield return new WaitForSeconds(0.05f);
        }
    }
    void StartDialogue()
    {
        dialogueStart = true;
        dialoguePanelText.SetActive(true);
        dialogueIcon.SetActive(false);
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
            dialogueIcon.SetActive(true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer==LayerMask.NameToLayer("TargetPlayer"))
        {
            dialogueExist = true;
            dialogueIcon.SetActive(false);
        }
     
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("TargetPlayer"))
        {
            dialogueExist = false;
            dialogueIcon.SetActive(true);
            dialoguePanelText.SetActive(false);

        }
    }
}
