using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUIController : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private BattleManager battleManager;

    [SerializeField] private string TitleSceneName = "TitleScene";
    [SerializeField] private string StageSelectSceneName = "LevelSelectScene";

    private bool mIsPaused = false;

    private void Awake()
    {
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (battleManager != null && battleManager.Finished)
            {
                return;
            }
            TogglePause();
        }
    }

    void TogglePause()
    {
        mIsPaused = !mIsPaused;

        pausePanel.SetActive(mIsPaused);

        if (mIsPaused)
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void OnClickResume()
    {
        if (battleManager != null && battleManager.Finished)
        {
            return;
        }
        TogglePause();
    }

    public void OnClickHome()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(TitleSceneName);
    }

    public void OnClickStageSelect()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(StageSelectSceneName);
    }
}
