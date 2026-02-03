using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUIController : MonoBehaviour
{
    [SerializeField] private string MainSceneName = "LevelSelectScene";

    public void OnClickStart()
    {
        SceneManager.LoadScene(MainSceneName);
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }
}
