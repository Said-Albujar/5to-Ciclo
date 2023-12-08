using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class vajuego : MonoBehaviour
{
   public void EscenaJuego()
    {
        SceneManager.LoadScene("Prototipo_Promedio2");
    }
    public void CargarNivel(string nombredescena)
    {
        SceneManager.LoadScene(nombredescena);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
