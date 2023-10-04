using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class NumericKeypadUI : MonoBehaviour
{
    [Header("Text Fields")]
    public TextMeshProUGUI inputText;
    public TextMeshProUGUI resultText;
    private string inputString = "";

    [Header("Password Configuration")]
    [SerializeField]
    private string contrase�aCorrecta = "1234";
    [SerializeField]
    private InputField passwordInputField;

    [Header("Object to Control")]
    public InteractiveDoor interactiveDoor;
    public void OnNumericButtonClicked(string digit)
    {
        inputString += digit;
        inputText.text = inputString;
    }
    public void OnClearButtonClicked()
    {
        inputString = "";
        inputText.text = inputString;
    }
    public void OnConfirmButtonClicked()
    {
        string contrase�aIngresada = inputString;

        inputString = "";
        inputText.text = inputString;
        if (Regex.IsMatch(contrase�aIngresada, "^[0-9]+$"))
        {
            if (contrase�aIngresada.Length == contrase�aCorrecta.Length)
            {
                if (contrase�aIngresada == contrase�aCorrecta)
                {
                    // Cambiar el estado de apertura de la puerta a "true"
                    if (interactiveDoor != null)
                    {
                        interactiveDoor.open = true;
                    }

                    resultText.text = "Contrase�a correcta";
                    Debug.Log("Contrase�a correcta. �Puerta abierta!");
                }
                else
                {
                    resultText.text = "Contrase�a incorrecta";
                    Debug.Log("Contrase�a incorrecta. La puerta permanece cerrada.");
                }
            }
            else
            {
                resultText.text = "Longitud de contrase�a incorrecta";
                Debug.Log("Longitud de contrase�a incorrecta.");
            }
        }
        else
        {
            resultText.text = "La contrase�a debe contener solo n�meros";
            Debug.Log("La contrase�a debe contener solo n�meros.");
        }

    }
}

