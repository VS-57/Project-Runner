using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchRocket : MonoBehaviour
{
    public Transform Rocket;
    public Transform boxx;
    public GameObject cherryBombPrefab;
    public GameObject blockerbox;


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
    }
    void Shoot()
    {
        Instantiate(cherryBombPrefab, Rocket.position, Rocket.rotation);
    }

    void Drop()
    {
        Instantiate(blockerbox, boxx.position, boxx.rotation);
    }
}
