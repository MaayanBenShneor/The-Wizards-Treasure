using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public GameObject fadeOut;
    public GameObject fadeIn;
    AudioManager audioManager;

    private void Awake() 
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Start() 
    {
        fadeIn.SetActive(true);
        audioManager.Stop("MainTheme");
        audioManager.Play("StartMenuTheme");
        audioManager.SetVolume("StartMenuTheme", 0.2f);
    }

    public void QuitGame()
    {
        audioManager.Play("ButtonClick");
        Debug.Log("Quitting game...");
        Application.Quit();
    }

    IEnumerator LoadMenu()
    {
        fadeOut.SetActive(true);
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene("StartMenu");
    }
}
