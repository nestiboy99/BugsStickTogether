using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Spawner : MonoBehaviour
{

    public Transform[] spawnPoints;
    public GameObject enemy;

    public float startTimeBetweenSpawns;
    float timeBetweenSpawns;

    private void Start()
    {
        timeBetweenSpawns = startTimeBetweenSpawns;
    }

    private void Update()
    {
        if(PhotonNetwork.IsMasterClient == false || PhotonNetwork.CurrentRoom.PlayerCount != 2)
        {
            return;
        }
        
        
        if(timeBetweenSpawns <= 0)
        {
            Vector3 spawnPosition = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
            PhotonNetwork.Instantiate(enemy.name, spawnPosition, Quaternion.identity );
            timeBetweenSpawns = startTimeBetweenSpawns;
        }
        else
        {
            timeBetweenSpawns -= Time.deltaTime;
        }
    }
}
