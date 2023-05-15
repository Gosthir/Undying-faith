using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSpawner : MonoBehaviour
{
    public GameObject laser;
    public float heightOffSet = 9;
    public float spawnrate = 2;
    public float timer = 0;
    public float speedofpipes = 10;


    // Start is called before the first frame update
    void Start()
    {
        Tworzenie();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnrate)
        {
            timer = timer + Time.deltaTime;
        }
        else
        {
            timer = 0;
            Tworzenie();
        }
    }

    void Tworzenie()
    {
        float lowestpoint = transform.position.y - heightOffSet;
        float highestpoint = transform.position.y + heightOffSet;
        speedofpipes = speedofpipes + 0.1f;
        spawnrate = spawnrate - 0.001f;

        Instantiate(laser, new Vector3(transform.position.x, UnityEngine.Random.Range(lowestpoint, highestpoint), 0), transform.rotation);

    }

}
