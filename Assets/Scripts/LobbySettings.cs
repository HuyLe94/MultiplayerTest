using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;

public class LobbySettings : MonoBehaviourPunCallbacks
{

    [SerializeField] private Text[] Username;
    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Text errorText;

    MainMenu menu = new MainMenu();
    
    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
        PhotonNetwork.AutomaticallySyncScene = true;
        for (int x = 0; x < PhotonNetwork.PlayerList.Length; x++)
        {
            Username[x].text = PhotonNetwork.PlayerList[x].ToString();
        }

        if (PhotonNetwork.IsMasterClient == true)
        {
            startButton.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame

    void Update()
    {
        
    }
    
    public void startGame()
    {
        Debug.Log(PhotonNetwork.CurrentRoom.MaxPlayers);
        Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount);
        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            PhotonNetwork.LoadLevel("MainGame");
        }
        else
        {
            string msg = "Not enough player";
            StartCoroutine(menu.secondDelay(msg, errorText));
        }
        
    }

    public void quitLobby()
    {
        PhotonNetwork.LoadLevel("Main Menu");
    }

    public override void OnJoinedRoom()
    {


        //base.OnJoinedRoom();
        
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        
        for (int x = 0; x < PhotonNetwork.PlayerList.Length; x++)
        {
            Username[x].text = PhotonNetwork.PlayerList[x].ToString();
        }

        if (PhotonNetwork.IsMasterClient == true)
        {
            startButton.gameObject.SetActive(true);
        }
    }
}
