using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpTrigger : MonoBehaviour
{
    public float duration = 4f;
    public float duration2 = 10f;
    public float speedboost = 1f;

    public int triggerPU;

    public GameObject BlockBox;
    public GameObject pickupEffect;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(SpeedUp(other));
            if (FindObjectOfType<PowerUps>().randomPU == 0)
            {
                FindObjectOfType<PowerUps>().randomPU = Random.Range(0, 101);
                FindObjectOfType<PowerUps>().temp = FindObjectOfType<PowerUps>().randomPU / 3;
            }

        }
    }

    IEnumerator SpeedUp(Collider2D player)
    {
        Playercontroller stats = player.GetComponent<Playercontroller>();
        Instantiate(pickupEffect, transform.position, transform.rotation);
        stats.movementSpeed *= speedboost;

        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;

        yield return new WaitForSeconds(duration);
        stats.movementSpeed = 3f;

        Destroy(gameObject);
    }
}
