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
    public float hp = 20;
    public float speed = 1;
    public float ammo = 3;
    public float rechargeRate = 3;
    public float bulletSpeed = 5;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletSpot;

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
        followMouse();
        reload();
        //Debug.Log(Mathf.Sqrt(rb.velocity.x*rb.velocity.x + rb.velocity.y*rb.velocity.y));
    }

    private void checkInputs()
    {
        if (Input.GetMouseButtonDown(0) && moved)
        {
            var mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            mousePosition = mousePosition - transform.position;
            rb.velocity = new Vector2(mousePosition.x, mousePosition.y).normalized * speed;
            
        }
        if (Input.GetMouseButtonDown(1) && ammo > 0)
        {
            shoot();
        }
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
                rb.velocity = new Vector2(mousePosition.x / (Mathf.Sqrt((mousePosition.x * mousePosition.x) + (mousePosition.y * mousePosition.y))) * speed,
                                          mousePosition.y / (Mathf.Sqrt((mousePosition.x * mousePosition.x) + (mousePosition.y * mousePosition.y))) * speed);
            }
            moved = true;
        }
        else
        {
            checkInputs();
        }
    }

    private void followMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 direction = new Vector2(mousePos.x - transform.position.x, mousePos.y-transform.position.y);
        transform.up = direction;
    }

    private void shoot()
    {
        
            Debug.Log("Shooting");
            GameObject a =Instantiate(bullet, bulletSpot.position, Quaternion.identity);
            a.GetComponent<BulletScript>().parent = gameObject;
            a.GetComponent<Rigidbody2D>().velocity = new Vector2(Input.mousePosition.x, Input.mousePosition.y).normalized * bulletSpeed;
            ammo--;
            //Debug.Log(ammo);
    }

    private void reload()
    {
        while (ammo < 3)
        {
            rechargeRate -= 1 * Time.deltaTime;
            Debug.Log(rechargeRate);
            if (rechargeRate <= 0)
            {
                ammo++;
                rechargeRate = 3;
            }
        }
    }
}
