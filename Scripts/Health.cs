using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class Health : MonoBehaviour
{

    public TextMeshProUGUI healthDisplay;
    public int health = 10;

    PhotonView view;

    public GameObject gameOver;
    

    private void Start()
    {
        view = GetComponent<PhotonView>();
        
    }

    public void TakeDamage()
    {
        view.RPC("TakeDamageRPC", RpcTarget.All);
    }

    [PunRPC]
    void TakeDamageRPC()
    {
        health--;

        if(health <= 0)
        {
            gameOver.SetActive(true);
            
        }

        healthDisplay.text = health.ToString();
    }



}
