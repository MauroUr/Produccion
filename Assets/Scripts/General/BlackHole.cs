using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlackHole : MonoBehaviour
{

    private float gravitationalForce = 9000f;
    private float timeSinceSpawned = 0;

    void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(this.transform.position, 7.5f);
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
        if (collision.gameObject.layer != 10)
            Destroy(collision.gameObject);
        else
            collision.gameObject.GetComponent<Player>().TakeDamage(100);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != 10 && collision.gameObject.tag != "Laser")
            Destroy(collision.gameObject);
    }
}
