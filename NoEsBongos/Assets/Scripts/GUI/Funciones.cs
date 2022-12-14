using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Funciones : MonoBehaviour
{
   public void Torna()
    {
        SceneManager.LoadScene("Juego");
    }
}
