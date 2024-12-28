using UnityEngine;

public class StartMenuTheme : MonoBehaviour
{
    AudioManager audioManager;

    private void Awake() 
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    void Start()
    {
        audioManager.Play("StartMenuTheme");
    }

    public void EndSound()
    {
        audioManager.FadeOut("StartMenuTheme");
    }
}
