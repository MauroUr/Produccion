using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : MonoBehaviour
{
    private Player _player;
    private float speed;
    private Rigidbody2D rb;

    void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        speed = _player.GetPlayerBSpeed();
        rb = GetComponent<Rigidbody2D>();

        Vector3 direction = _player.transform.position - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        this.transform.rotation = Quaternion.Euler(0f, 0f, angle );
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = (this.transform.right).normalized * speed * Time.deltaTime * 50;

        if (Vector2.Distance(transform.position, _player.gameObject.transform.position) > 200)
            Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            _player.TakeDamage(20);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            _player.TakeDamage(20);
        }
    }
}
