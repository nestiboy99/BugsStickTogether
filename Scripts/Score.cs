using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class Score : MonoBehaviour
{

    public int score = 0;
    PhotonView view;
    public TextMeshProUGUI scoreDisplay;

    private void Start()
    {
        view = GetComponent<PhotonView>();
    }

    public void AddScore()
    {
        view.RPC("AddScoreRPC", RpcTarget.All);
    }

    [PunRPC]
    void AddScoreRPC()
    {
        score++;
        scoreDisplay.text = score.ToString();
    }


}
