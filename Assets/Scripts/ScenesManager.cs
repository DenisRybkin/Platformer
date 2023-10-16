using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public void StartMainScene ()
    {
        SceneManager.LoadScene(0);
    }

    public static void StaticStartMenuScene()
    {
        SceneManager.LoadScene(1);
    }

    public static void AplicationExit()
    {
        Application.Quit();
    }
}
