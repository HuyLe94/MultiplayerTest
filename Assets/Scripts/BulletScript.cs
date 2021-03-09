using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletScript : MonoBehaviourPun 
{ 
    //public int parent;
    private PhotonView PV;
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [PunRPC]
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PhotonView>().Owner != PV.Owner || collision.CompareTag("Wall"))
            Destroy(gameObject);
    }

    private void findParent()
    {
       
    }

}
