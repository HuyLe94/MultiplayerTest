using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject players;
    public Transform[] playersSpots;
    public GameObject personalCams;

    private void Start()
    {
        spawnPlayers();
    }

    public void spawnPlayers()
    {
        int a = Random.Range(0, 4);
        PhotonNetwork.Instantiate(players.name, this.playersSpots[a].position, Quaternion.identity);
    }
}
