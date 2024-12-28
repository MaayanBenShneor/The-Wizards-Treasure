using UnityEngine;

public class ThemeSong : MonoBehaviour
{
    AudioManager audioManager;
    public static ThemeSong instance;

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

    private void Start() 
    {
        audioManager.Stop("StartMenuTheme");
        audioManager.Play("MainTheme");
    }
}
