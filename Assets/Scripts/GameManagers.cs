using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class GameManagers : MonoBehaviour
{
    public GameObject players;
    public Transform[] playersSpots;
    [SerializeField] private Camera MainCam;
    [SerializeField] private Camera PersonalCam;
    // Start is called before the first frame update
    void Start()
    {
        //MainCam.gameObject.SetActive(false);
        //PersonalCam.gameObject.SetActive(true);
        spawnPlayers();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnPlayers()
    {
        int a = Random.Range(0, 4);
        //PhotonNetwork.Instantiate(players.name, this.playersSpots[a].position, Quaternion.identity);
        PhotonNetwork.Instantiate(Path.Combine("Prefabs","player"), this.playersSpots[a].position, Quaternion.identity);
    }
}
