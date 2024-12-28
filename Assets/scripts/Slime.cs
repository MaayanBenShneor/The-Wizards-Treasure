using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    PlayerMovement player;
    public float slimeBounceForce;

    private void Start() 
    {
        player = FindObjectOfType<PlayerMovement>();
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            player.Slime(slimeBounceForce);
        }
    }
}
