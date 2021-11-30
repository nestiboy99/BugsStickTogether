using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;

public class ConnectToServer : MonoBehaviourPunCallbacks
{

    void Start()
    {
        //new stuff
        PhotonNetwork.AutomaticallySyncScene = true;

        PhotonNetwork.ConnectUsingSettings();
    }


    public override void OnConnectedToMaster()
    {
        Debug.Log("[Network Manager]: Connected to " + PhotonNetwork.CloudRegion + "server");
        SceneManager.LoadScene("Main Menu");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarning("[Network Manager]: disconnected with reason " + cause);
    }


}
