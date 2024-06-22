using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lasers : MonoBehaviour
{
    private Player _player;
    private float speed;
    private float lastDamage = 2.1f;
    private Rigidbody2D rb;

    void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        speed = _player.GetPlayerBSpeed() * 0.9f;
        rb = GetComponent<Rigidbody2D>();

        Vector3 direction = _player.transform.position - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        this.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
    void FixedUpdate()
    {
        rb.velocity = (this.transform.right).normalized * speed * Time.deltaTime * 50;
    
        if (Vector2.Distance(transform.position, _player.gameObject.transform.position) > 200)
            Destroy(this.gameObject);
    }
    void Update()
    {
        lastDamage += Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() && lastDamage > 2f)
        {
            lastDamage = 0;
            _player.TakeDamage(20);
        }
    }
}
