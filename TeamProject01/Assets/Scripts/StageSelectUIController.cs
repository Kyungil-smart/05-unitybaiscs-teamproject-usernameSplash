using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectUIController : MonoBehaviour
{
    [Header("Scene Name")]
    [SerializeField] private string TitleSceneName = "TitleScene";
    [SerializeField] private string Stage1SceneName = "GameStageScene1";
    [SerializeField] private string Stage2SceneName = "GameStageScene2";
    [SerializeField] private string Stage3SceneName = "GameStageScene3";
    [SerializeField] private string Stage4SceneName = "GameStageScene4";

    public void BackToMain() => Load(TitleSceneName);

    public void LoadStage1() => Load(Stage1SceneName);
    public void LoadStage2() => Load(Stage2SceneName);
    public void LoadStage3() => Load(Stage3SceneName);
    public void LoadStage4() => Load(Stage4SceneName);

    private void Load(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
