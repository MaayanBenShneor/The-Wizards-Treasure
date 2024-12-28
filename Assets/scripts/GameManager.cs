using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public AudioManager audioManager;
    public Animator transition;

    [HideInInspector]
    public bool playerIsDead = false;
    bool gameHasEnded;
    Vector2 startPosition;

    private void Awake() 
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Start() 
    {
        playerIsDead = false;

        startPosition = player.transform.position;
    }

    private void Update()
    {
        if(player.transform.position.y <= -9)
        {
            playerIsDead = true;
        }
            
        if(playerIsDead == true)
        {
            if(gameHasEnded == false)
            {
                gameHasEnded = true;

                audioManager.Play("PlayerDeath");
                audioManager.Stop("TvStatic"); //makes sure that Tv static is off

                StartCoroutine(RestartLevel());
            }
        }
    }

    IEnumerator RestartLevel()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(.5f);
        gameHasEnded = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void callNextLevel()
    {
        StartCoroutine(LoadNextLevel());
    }

    IEnumerator LoadNextLevel()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
