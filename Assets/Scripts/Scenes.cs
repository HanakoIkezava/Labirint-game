using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    public void OpenGame()
    {
        SceneManager.LoadScene("Maze 3d");
    }
    public void OpenMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
