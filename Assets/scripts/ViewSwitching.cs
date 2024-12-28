using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewSwitching : MonoBehaviour
{
    public GameObject layout;
    public GameObject postProcessing;
    public Animator postProcessingAnim;
    public Text filmsLeft;
    AudioManager audioManager;
    
    public float timeToClose;
    public int availableSwitches;

    [HideInInspector]
    public bool onLayout = false;
    
    private void Start() 
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.E) && availableSwitches > 0 && onLayout == false)
        {
            audioManager.Play("TvOn");

            layout.SetActive(true);
            onLayout = true;
            postProcessing.SetActive(true);

            StartCoroutine(CloseLayout());
            availableSwitches--;
        }

        filmsLeft.text = "Camera Films Left: " + availableSwitches;
    }

    IEnumerator CloseLayout()
    {
        audioManager.Play("TvStatic");

        yield return new WaitForSeconds(timeToClose);

        audioManager.Stop("TvStatic");
        audioManager.Play("TvOff");
        postProcessingAnim.SetBool("layoutClosed", true);

        yield return new WaitForSeconds(.2f);

        layout.SetActive(false);
        onLayout = false;
        postProcessing.SetActive(false);
    }
}
