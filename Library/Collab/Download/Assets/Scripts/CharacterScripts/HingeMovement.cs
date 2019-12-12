using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HingeMovement : MonoBehaviour
{
    // Start is called before the first frame update

    private bool _grabbing;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _grabbing = true;
            Debug.Log("Çalışıyor");
        }
        if (Input.GetMouseButtonDown(0))
        {
            GameObject closest = FindNearest();

            if (_grabbing)
            {
                closest.GetComponentInChildren<HingeJoint2D>().connectedBody = gameObject.GetComponentInChildren<Rigidbody2D>();
                _grabbing = false;
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            GameObject[] hinges;
            hinges = GameObject.FindGameObjectsWithTag("hinge");

            foreach(GameObject go in hinges)
            {
                go.GetComponentInChildren<HingeJoint2D>().connectedBody = null;
            }
        }
    }

    GameObject FindNearest()
    {
        GameObject[] hinges;
        hinges = GameObject.FindGameObjectsWithTag("hinge");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        Debug.Log("Çalışıyor2");

        foreach (GameObject go in hinges)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if(curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }

        return closest;
    }
}
