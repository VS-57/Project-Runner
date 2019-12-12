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

    public double temp;
    public double randomPU = 0;

    void Update()
    {
        if (Input.GetKeyDown("e") && (randomPU > 0 && randomPU <= 30 ))
        {
            Drop();
            randomPU -= temp;
        }
        if (Input.GetKeyDown("e") && (randomPU >= 31 && randomPU <= 62))
        {
            Shoot();
            randomPU = 0;
        }
        if (Input.GetKeyDown("e") && (randomPU >= 63 && randomPU <=100))
        {
            IceShoot();
            randomPU = 0;
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
