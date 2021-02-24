using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class Player : MonoBehaviourPun
{
    private Rigidbody2D rb;
    private bool moved = false;
    public Camera selfCam;
    public float atk = 10;
    public float hp =20;
    public float speed = 1;
    public float ammo = 3;
    public float rechargeRate =3;
    public float bulletSpeed =1;

    public PhotonView pView;

    private void Awake()
    {
        
    }
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        moving();
    }

    private void checkInputs()
    {
        
    }

    private void moving()
    {
        if (!moved)
        {
            if (Input.GetMouseButtonDown(0))
            {
                var mousePosition = Input.mousePosition;
                mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
                mousePosition = mousePosition - transform.position;
                rb.velocity = new Vector2(mousePosition.x/(Mathf.Sqrt((mousePosition.x* mousePosition.x) +(mousePosition.y* mousePosition.y)))*speed,
                                          mousePosition.y / (Mathf.Sqrt((mousePosition.x * mousePosition.x) + (mousePosition.y * mousePosition.y)))*speed);
            }
            moved = false;
        }
        else
        {
            checkInputs();
        }
    }
}
