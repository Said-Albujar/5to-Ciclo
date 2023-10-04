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
    private string contraseñaCorrecta = "1234";
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
        string contraseñaIngresada = inputString;

        inputString = "";
        inputText.text = inputString;
        if (Regex.IsMatch(contraseñaIngresada, "^[0-9]+$"))
        {
            if (contraseñaIngresada.Length == contraseñaCorrecta.Length)
            {
                if (contraseñaIngresada == contraseñaCorrecta)
                {
                    // Cambiar el estado de apertura de la puerta a "true"
                    if (interactiveDoor != null)
                    {
                        interactiveDoor.open = true;
                    }

                    resultText.text = "Contraseña correcta";
                    Debug.Log("Contraseña correcta. ¡Puerta abierta!");
                }
                else
                {
                    resultText.text = "Contraseña incorrecta";
                    Debug.Log("Contraseña incorrecta. La puerta permanece cerrada.");
                }
            }
            else
            {
                resultText.text = "Longitud de contraseña incorrecta";
                Debug.Log("Longitud de contraseña incorrecta.");
            }
        }
        else
        {
            resultText.text = "La contraseña debe contener solo números";
            Debug.Log("La contraseña debe contener solo números.");
        }

    }
}

