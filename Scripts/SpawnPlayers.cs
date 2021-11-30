using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{

    //public GameObject player;
    public float minX, minY, maxX, maxY;

    //not sure
    public GameObject[] players;
    GameObject currenPlayer;
    int index;

    private void Start()
    {
        Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));

        //not sure
        index = Random.Range(0, players.Length);
        currenPlayer = players[index];

        PhotonNetwork.Instantiate(currenPlayer.name, randomPosition, Quaternion.identity);

        //PhotonNetwork.Instantiate(player.name, randomPosition, Quaternion.identity);
    }
}
