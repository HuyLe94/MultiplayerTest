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


    private void Awake()
    {
        for(int x=0; x< PhotonNetwork.PlayerList.Length; x++)
        {
            Username[x].text = PhotonNetwork.PlayerList[x].ToString();
        }

        if (PhotonNetwork.IsMasterClient == true)
        {
            startButton.gameObject.SetActive(true);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

    void Update()
    {
        
    }
    
    private void startGame()
    {

    }
}
