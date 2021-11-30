using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;


public class MainMenu : MonoBehaviourPunCallbacks
{
  
    
    public TMP_InputField createInput;
    public TMP_InputField joinInput;

    public TMP_InputField nameInput;

    //new stuff
    public GameObject lobbyPanel;
    public GameObject roomPanel;
    public TextMeshProUGUI roomName;

    public GameObject playButton;
    public GameObject playMessage;


    public void Start()
    {
        PhotonNetwork.JoinLobby();
    }

    public void ChangeName()
    {
        PhotonNetwork.NickName = nameInput.text;
    }

    public void CreateRoom()
    {
       if(createInput.text.Length >= 1)
        {
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 2;
            PhotonNetwork.CreateRoom(createInput.text, roomOptions);
            Debug.Log("Has creado una sala to guapa");
        } 
 
    }

  

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom()
    {
        //new stuff
        lobbyPanel.SetActive(false);
        roomPanel.SetActive(true);
        roomName.text = "Room Name: " + PhotonNetwork.CurrentRoom.Name;

        //PhotonNetwork.LoadLevel("Game");
    }

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount >= 2)
        {
            playButton.SetActive(true);
            playMessage.SetActive(false);
        }
        else
        {
            playButton.SetActive(false);
            playMessage.SetActive(true);

        }
    }

    public void OnClickPlayButton()
    {
        PhotonNetwork.LoadLevel("Game");
    }


}
