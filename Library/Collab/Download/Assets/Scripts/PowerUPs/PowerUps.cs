using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public Transform Rocket;
    public Transform Dropbox;
    public Transform Ice;

    public GameObject Rocket1;
    public GameObject Dropbox1;
    public GameObject Ice1;


    void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            Shoot();
        }
        if (Input.GetKeyDown("e"))
        {
            Drop();
        }
        if (Input.GetKeyDown("r"))
        {
            IceShoot();
        }

    }
    void Shoot()
    {
        Instantiate(Rocket1, Rocket.position, Rocket.rotation);
    }

    void Drop()
    {
        Instantiate(Dropbox1, Dropbox.position, Dropbox.rotation);
    }
    void IceShoot()
    {
        Instantiate(Ice1, Ice.position, Ice.rotation);
    }
}
