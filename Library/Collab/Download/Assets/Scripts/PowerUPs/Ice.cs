using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ICE : MonoBehaviour
{
    public Transform ice;
    public Transform icepiece;
    public GameObject iceimg;


    void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            Shoot();
        }
    }
    void Shoot()
    {
        Instantiate(iceimg, ice.position, ice.rotation);
    }
}

