using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DripstoneSpawner : MonoBehaviour
{
    public GameObject dripstone;

    public float timeBetweenSpawns;
    public bool delayOnStart;
    public float startDelayTime;

    private void Start() 
    {
        StartCoroutine(SpawnDripstone());
    }

    IEnumerator SpawnDripstone()
    {
        if(delayOnStart)
        {
            yield return new WaitForSeconds(startDelayTime);
            delayOnStart = false;
        }

        GameObject spawnedDripstone = Instantiate(dripstone, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(timeBetweenSpawns);
        Destroy(spawnedDripstone);
        StartCoroutine(SpawnDripstone());
    }
}
