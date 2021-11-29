using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenuPanel;
    [SerializeField] private float TimeToPopUp;
    private MenuController _menuController;
    private void Awake()
    {
        _menuController = PauseMenuPanel.GetComponent<MenuController>();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(PauseMenuPanel.activeSelf)
            {                
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void Resume()
    {
        _menuController.SetActive(false, TimeToPopUp);
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void Pause()
    {
        _menuController.SetActive(true, TimeToPopUp);
        StartCoroutine(DelayPauseResume(0, TimeToPopUp));
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    IEnumerator DelayPauseResume(float timeScale, float delay)
    {
        yield return new WaitForSeconds(delay);
        Time.timeScale = timeScale;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
