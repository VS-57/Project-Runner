using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitFinishLine : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject hitobj = collider.gameObject;

        if (hitobj.tag == "Player")
        {
            Debug.Log("Kazandın");
        }
    }


}
