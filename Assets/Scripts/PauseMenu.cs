using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Internal.Experimental.UIElements;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public static bool gamePaused = false;
    public GameObject pauseMenuUI;

   private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (gamePaused) {
                Resume();
            } else {
                Pause();
            }
        }
    }

    public void Pause() {
        gamePaused = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }
    
    public void Resume() {
        gamePaused = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Restart() {
        gamePaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    public void MainMenu() {
        gamePaused = false;
        SceneManager.LoadScene("MainMenu");
        
        Time.timeScale = 1f;
    }
}
