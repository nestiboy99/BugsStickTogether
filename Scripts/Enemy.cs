using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Enemy : MonoBehaviour
{
    PlayerController[] players;
    PlayerController nearestPlayer;
    public float speed;
    Score score;

    public GameObject deathFX;
    PhotonView view;

    private void Start()
    {
        view = GetComponent<PhotonView>();
        players = FindObjectsOfType<PlayerController>();
        score = FindObjectOfType<Score>();

    }

    private void Update()
    {
        float distanceOne = Vector2.Distance(transform.position, players[0].transform.position);
        float distanceTwo = Vector2.Distance(transform.position, players[1].transform.position);

        if(distanceOne < distanceTwo)
        {
            nearestPlayer = players[0];
        }
        else
        {
            nearestPlayer = players[1];
        }

        if(nearestPlayer != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, nearestPlayer.transform.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (collision.tag == "GoldenRay")
            {
                score.AddScore();
                view.RPC("SpawnParticle", RpcTarget.All);
                PhotonNetwork.Destroy(this.gameObject);
            }
        }

       
    }

    [PunRPC]
    void SpawnParticle()
    {
        Instantiate(deathFX, transform.position, Quaternion.identity);
    }



}
