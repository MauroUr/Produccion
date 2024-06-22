using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : MonoBehaviour
{
    private Player _player;
    //private float speed = 2;
    private Rigidbody2D rb;
    private bool hasDoneDamage = false;
    private float timeSinceSpawned = 0;

    void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();

        Vector3 direction = _player.transform.position - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        this.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        rb.AddForce(direction/2, ForceMode2D.Impulse);
    }

    private void Update()
    {

        if (Vector2.Distance(transform.position, _player.gameObject.transform.position) > 200 || timeSinceSpawned > 10)
            Destroy(this.gameObject);
        timeSinceSpawned += Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() && !hasDoneDamage)
        {
            _player.TakeDamage(20);
            hasDoneDamage = true;
            AudioManager.instance.PlaySound("MeteoriteExplosion");
        }
    }
}
