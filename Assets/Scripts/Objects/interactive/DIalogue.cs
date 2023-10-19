using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DIalogue : MonoBehaviour
{
    public TextMeshProUGUI dialoqueText;
    public GameObject boxDialogue;
    public string[] lines;
    public float speed;
    int index;
    public bool dialogueExist;
    // Start is called before the first frame update
    void Start()
    {
        boxDialogue.SetActive(false);
        dialoqueText.text = string.Empty;

    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueExist)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if(dialoqueText.text==lines[index])
                {
                    NextDialogue();
                }
                else
                {
                    boxDialogue.SetActive(true);
                    StartDialogue();
                    StopAllCoroutines();
                    dialoqueText.text = lines[index];
                }
            

            }
        }
        else
        {
            boxDialogue.SetActive(false);

        }
    }
    public void StartDialogue()
    {
        index = 0;
        StartCoroutine(ShowLine());
    }
    IEnumerator ShowLine()
    {
        foreach(char letter in lines[index])
        {
            dialoqueText.text += letter;
            yield return new WaitForSeconds(speed);
        }
    }
    private void NextDialogue()
    {
        if(index<lines.Length-1)
        {
            index++;
            dialoqueText.text = string.Empty;
            StartCoroutine(ShowLine());

        }
        else
        {
            boxDialogue.SetActive(false);

        }
    }
  
  
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Detected"))
        {
            dialogueExist = true;
        }
     
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Detected"))
        {
            dialogueExist = false;
        }
    }
}
