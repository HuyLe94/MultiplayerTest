using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class GameManagers : MonoBehaviour
{
    private PlayersStats stat;
    private int teamA;
    private int teamB;
    [SerializeField] public Transform[] playersSpots;
    [SerializeField] private Camera MainCam;
    [SerializeField] private Camera PersonalCam;
    // Start is called before the first frame update
    void Start()
    {
        stat = new PlayersStats();
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
        if(playersSpots[a].position != new Vector3(0, 0, 0))
        {
            GameObject b = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "player"), playersSpots[a].position, Quaternion.identity);
            AssignTeam(b);
            playersSpots[a].position = new Vector3(0,0,0); 
        }
        else
        {
            spawnPlayers();
        }
    }

    public void AssignTeam(GameObject player)
    {
        int b = Random.Range(0, 2);
        if(b == 0)
        {
            if (teamA < PhotonNetwork.CurrentRoom.PlayerCount / 2)
            {
                player.GetComponent<Player>().side = Player.team.TeamA;
                teamA++;
            }
            else
            {
                player.GetComponent<Player>().side = Player.team.TeamB;
                teamB++;
            }
        }
        else
        {
            if (teamB < PhotonNetwork.CurrentRoom.PlayerCount / 2)
            {
                player.GetComponent<Player>().side = Player.team.TeamB;
                teamB++;
            }
            else
            {
                player.GetComponent<Player>().side = Player.team.TeamA;
                teamA++;
            }
        }
    }
}
