using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuPanel;
    public Button pauseButton;
    public Button resumeButton;

    private bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenuPanel.SetActive(false);

        pauseButton.onClick.AddListener(PauseGame);
        resumeButton.onClick.AddListener(ResumeGame);
    }


    // Update is called once per frame
    void Update()
    {

    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
        pauseMenuPanel.SetActive(true);
        pauseButton.gameObject.SetActive(false);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
        pauseMenuPanel.SetActive(false);
        pauseButton.gameObject.SetActive(true);
    }
}

