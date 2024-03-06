using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void SceneChange(string scenename)
    {
        SceneManager.LoadSceneAsync(scenename);
    }

    public void GameExit()
    {
        Application.Quit();
    }
}