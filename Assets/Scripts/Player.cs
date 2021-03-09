using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class Player : MonoBehaviourPun
{
    public PhotonView PV;
    private Rigidbody2D rb;
    public enum team { TeamA, TeamB };
    public team side;
    private bool moved = false;
    public Camera selfCam;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletSpot;
    PlayersStats stats;


    
    private float temp;


    private void Awake()
    {

    }
    
    private void Start()
    {
        //side = team.TeamA;
        stats = new PlayersStats();
        if (PV.IsMine)
        {
            rb = gameObject.GetComponent<Rigidbody2D>();
        }
        temp = stats.rechargeRate;
    }
    
    private void Update()
    {
        if(PV.IsMine)
        {
            if (Input.GetMouseButtonDown(1) && stats.ammo > 0)
            {
                shoot();
            }
            checkInputs();
            followMouse();
            reload();

            if (Input.GetKeyDown(KeyCode.H))
            {
                stats.maxAmmo += 1;
            }
        }
        
    }

    private void checkInputs()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            mousePosition = mousePosition - transform.position;
            rb.velocity = new Vector2(mousePosition.x, mousePosition.y).normalized * stats.speed;
            
        }
        
    }

    [PunRPC]
    private void moving()
    {
        if (!moved)
        {
            if (Input.GetMouseButtonDown(0))
            {
                var mousePosition = Input.mousePosition;
                mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
                mousePosition = mousePosition - transform.position;
                rb.velocity = new Vector2(mousePosition.x / (Mathf.Sqrt((mousePosition.x * mousePosition.x) + (mousePosition.y * mousePosition.y))) * stats.speed,
                                          mousePosition.y / (Mathf.Sqrt((mousePosition.x * mousePosition.x) + (mousePosition.y * mousePosition.y))) * stats.speed);
            }
            moved = true;
        }
    }
    [PunRPC]
    private void followMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 direction = new Vector2(mousePos.x - transform.position.x, mousePos.y-transform.position.y);
        transform.up = direction;
    }
    [PunRPC]
    private void shoot()
    {
        
        GameObject a =PhotonNetwork.Instantiate(Path.Combine("Prefabs", "TestBullet"), bulletSpot.position, Quaternion.identity);
        //a.GetComponent<BulletScript>().parent = gameObject.GetComponent<PhotonView>().GetInstanceID();
        
        var mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition = mousePosition - transform.position;
        a.GetComponent<Rigidbody2D>().velocity = new Vector2(mousePosition.x, mousePosition.y).normalized * stats.bulletSpeed;
        stats.ammo--;
    }
    [PunRPC]
    private void reload()
    {
        if (stats.ammo < stats.maxAmmo)
        {
            stats.rechargeRate -= Time.deltaTime;
            if (stats.rechargeRate <= 0)
            {
                stats.ammo++;
                stats.rechargeRate = temp;
            }
        }
    }
}
