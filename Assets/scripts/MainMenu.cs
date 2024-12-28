using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour
{   
    public AudioManager audioManager;
    public StartMenuTheme menuTheme;
    public GameObject creditsWindow;
    public GameObject ScreenWipe;
    
    public Animator playButton;
    public Animator creditsButton;
    public Animator quitButton;
    public Animator title;

    public void Play()
    {
        ScreenWipe.SetActive(true);
        audioManager.Play("ButtonClick");
        menuTheme.EndSound();
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Credits()
    {
        audioManager.Play("ButtonClick");
        gameObject.SetActive(false);
        creditsWindow.SetActive(true);

        playButton.enabled = false;
        creditsButton.enabled = false;
        quitButton.enabled = false;
        title.enabled = false;
    }

    public void BackToMenu()
    {
        audioManager.Play("ButtonClick");
        gameObject.SetActive(true);
        creditsWindow.SetActive(false);
    }

    public void QuitGame()
    {
        audioManager.Play("ButtonClick");
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}
