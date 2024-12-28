using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    public Animator oldManAnim;
    public Animator screenWipeAnim;
    public FadeInMainTheme fadeInTheme;
    AudioManager audioManager;

    Queue<string> sentences;
    public Dialogue dialogue;
    public Text dialogueText;

    private void Start() 
    {
        audioManager = FindObjectOfType<AudioManager>();

        Invoke("Intro", 2f);
        sentences = new Queue<string>();
        StartDialogue(dialogue);
    }

    void Intro()
    {
        oldManAnim.SetTrigger("Idle");
    }

    public void StartDialogue(Dialogue _dialogue)
    {
        sentences.Clear();

        foreach(string sentence in _dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            screenWipeAnim.SetTrigger("Start");

            audioManager.FadeOut("StartMenuTheme");
            Invoke("EndDialogue", .5f);

            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(.01f);
        }
    }

    void EndDialogue()
    {
        fadeInTheme.FadeInTheme();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            DisplayNextSentence();
        }
    }
}
