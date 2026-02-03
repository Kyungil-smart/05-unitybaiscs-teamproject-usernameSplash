using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUIController : MonoBehaviour
{
    [SerializeField] private string SceneName = "LevelSelectScene";

    public void OnClickStart()
    {
        SceneManager.LoadScene(SceneName);
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }
}
