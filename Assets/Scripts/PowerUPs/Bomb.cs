using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float speed = 4f;
    public Rigidbody2D rb;
    public float slowingDown = 1f;
    public float duration = 2f;
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Player"))
        {
            StartCoroutine(pUp(hitInfo));
        }
    }
    void Start()
    {
        rb.velocity = transform.right * speed;
    }
    IEnumerator pUp(Collider2D hitInfo)
    {
        Playercontroller pc = hitInfo.GetComponent<Playercontroller>();
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;

        if (pc != null)
        {
            pc.movementSpeed -= slowingDown;
            if (pc.movementSpeed <= 0f)
            {
                pc.movementSpeed = 1f;
                
            }
            yield return new WaitForSeconds(duration);
            pc.movementSpeed = 3f;
        }

        Destroy(gameObject);
    }
}
