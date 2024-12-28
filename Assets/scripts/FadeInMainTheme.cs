using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInMainTheme : MonoBehaviour
{
    AudioManager audioManager;

    public static FadeInMainTheme instance;

    private void Awake() 
    {
        audioManager = FindObjectOfType<AudioManager>();

        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void FadeInTheme()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        audioManager.SetVolume("MainTheme", 0.01f);
        yield return new WaitForSeconds(.25f);
        audioManager.SetVolume("MainTheme", 0.02f);
        yield return new WaitForSeconds(.25f);
        audioManager.SetVolume("MainTheme", 0.025f);
        yield return new WaitForSeconds(.25f);
        audioManager.SetVolume("MainTheme", 0.03f);
        yield return null;
        Destroy(gameObject);
    }
}
