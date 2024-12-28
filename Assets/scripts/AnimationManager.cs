using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public GameObject playButton;
    public GameObject creditsButton;
    public GameObject quitButton;

    public GameObject transition;
    
    void Start()
    {
        transition.SetActive(true);
        StartCoroutine(StartAnimation());
    }

    IEnumerator StartAnimation()
    {
        yield return new WaitForSeconds(1.5f);
        playButton.SetActive(true);
        yield return new WaitForSeconds(.4f);
        creditsButton.SetActive(true);
        yield return new WaitForSeconds(.4f);
        quitButton.SetActive(true);
    }
}
