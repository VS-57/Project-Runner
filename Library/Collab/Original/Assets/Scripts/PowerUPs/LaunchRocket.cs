using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchRocket : MonoBehaviour
{
    public Transform Rocket;
    public Transform boxx;
    public Transform ICE;
    public GameObject cherryBombPrefab;
    public GameObject blockerbox;
    public GameObject ICEIMG;
   


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
            Shoot2();
        }
    }
    void Shoot()
    {
        Instantiate(cherryBombPrefab, Rocket.position, Rocket.rotation);
    }

    void Drop()
    {
        Instantiate(blockerbox, boxx.position, boxx.rotation);
    }
    void Shoot2()
    {
        Instantiate(ICEIMG, ICE.position, ICE.rotation);
    }
}
