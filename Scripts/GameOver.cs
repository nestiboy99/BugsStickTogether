using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class GameOver : MonoBehaviour
{

    public TextMeshProUGUI scoreDisplay;
    public GameObject restartButton;
    public GameObject waitingText;

    PhotonView view;

    private void Start()
    {
        view = GetComponent<PhotonView>();

        scoreDisplay.text = FindObjectOfType<Score>().score.ToString();

        if(PhotonNetwork.IsMasterClient == false)
        {
            restartButton.SetActive(false);
            waitingText.SetActive(true);
        }
    }

    public void OnClickRestart()
    {
        view.RPC("Restart", RpcTarget.All);
    }

    [PunRPC]
    void Restart()
    {
        PhotonNetwork.LoadLevel("Game");
    }

}
