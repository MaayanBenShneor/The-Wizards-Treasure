using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTriggerer : MonoBehaviour
{
    public GameObject transition;
    AudioManager audioManager;

    private void Start() 
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            StartCoroutine(EndLevel());
        }
    }

    IEnumerator EndLevel()
    {
        audioManager.SetVolume("MainTheme", .01f);
        transition.SetActive(true);
        yield return new WaitForSeconds(1f);
        audioManager.SetVolume("MainTheme", .005f);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("EndScreen");
    }
}
