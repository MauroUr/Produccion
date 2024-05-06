using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{

    private float gravitationalForce = 2f;
    private float timeSinceSpawned = 0;

    void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(this.transform.position, 5f);

        foreach (Collider2D collider in colliders) { 
            if(collider.gameObject.GetComponent<Rigidbody2D>() != null)
                collider.gameObject.GetComponent<Rigidbody2D>().AddForce((transform.position - collider.transform.position).normalized 
                                                                            * gravitationalForce * Time.deltaTime);
        }
        timeSinceSpawned += Time.deltaTime;

        if (timeSinceSpawned > 20f)
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer != 10)
            Destroy(collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != 10)
            Destroy(collision.gameObject);
    }
}
