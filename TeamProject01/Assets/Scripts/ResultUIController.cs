using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ResultUIController : MonoBehaviour
{
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private TMP_Text titleText;

    [SerializeField] private string TitleSceneName = "TitleScene";
    [SerializeField] private string StageSelectSceneName = "LevelSelectScene";
    
    private Coroutine showCoroutine;
    
    private void Awake()
    {
        if (resultPanel != null)
        {
            resultPanel.SetActive(false);
        }
    }

    public void ShowAfterDelay(bool isWin)
    {
        StartCoroutine(CoShowAfterDelay(isWin));
    }

    private IEnumerator CoShowAfterDelay(bool isWin)
    {
        yield return new WaitForSecondsRealtime(2f);
        Show(isWin);
    }
    
    public void Show(bool isWin)
    {
        resultPanel.SetActive(true);

        titleText.text = isWin ? "VICTORY" : "DEFEAT";

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void OnClickTitle()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(TitleSceneName);
    }

    public void OnClickRetry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickStageSelect()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(StageSelectSceneName);
    } 
}
