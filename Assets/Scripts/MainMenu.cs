using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;

public class MainMenu : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    [SerializeField] private Text errorField;
    [SerializeField] private InputField playerNametxt;
    [SerializeField] private InputField joinRoomtxt;
    [SerializeField] private InputField hostRoomtxt;
    [SerializeField] private Dropdown mode;
    [SerializeField] private GameObject hostjoinPannel;


    [SerializeField] private Button setName;
    [SerializeField] private Button joinRoom;
    [SerializeField] private Button hostRoom;

    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings();
        hostjoinPannel.SetActive(false);
    }
    public void ChangeUserName()
    {
        if(playerNametxt.text != null && playerNametxt.text.Length > 3 && playerNametxt.text.Length < 8)
        {
            PhotonNetwork.NickName = playerNametxt.text;
            Debug.Log("Username is now: " + playerNametxt.text);
            hostjoinPannel.SetActive(true);
        }
        else
        {
            string error = "Please make sure to set your name. (Must be between 3-8 character)";
            StartCoroutine(secondDelay(error,errorField));

        }
    }

    public void CreateRoom()
    {
        if (mode.value == 0)
        {
            Debug.Log("Mode is " + mode.value.ToString());
            PhotonNetwork.CreateRoom(hostRoomtxt.text, new RoomOptions() { MaxPlayers = 1 }, null);

        }
        else if (mode.value == 1)
        {
            Debug.Log("Mode is " + mode.value.ToString());
            PhotonNetwork.CreateRoom(hostRoomtxt.text, new RoomOptions() { MaxPlayers = 2 }, null);
            
        }
        else if (mode.value == 2)
        {
            Debug.Log("Mode is " + mode.value.ToString());
            PhotonNetwork.CreateRoom(hostRoomtxt.text, new RoomOptions() { MaxPlayers = 4 }, null);
        }
    }
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinRoomtxt.text,null);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Joined "+joinRoomtxt.text+" successfully.");
        PhotonNetwork.LoadLevel("Lobby");
    }

    public IEnumerator secondDelay(string msg, Text txtbox)
    {
        txtbox.gameObject.SetActive(true);
        txtbox.text = msg;
        yield return new WaitForSeconds(5);
        txtbox.gameObject.SetActive(false);
        
    }
}
