using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollector : MonoBehaviour
{
    AudioManager audioManager;
    public Door door;

    private void Start() 
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            audioManager.Play("KeyCollected");
            door.locked = false;
            Destroy(gameObject);
        }
    }
}
