using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : MonoBehaviour
{
    private Player _player;
    private float speed;
    private Rigidbody2D rb;
    [SerializeField] private GameObject minePrefab;
    private bool plantedMine = false;

    void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        speed = _player.GetPlayerBSpeed() / 2;
        rb = GetComponent<Rigidbody2D>();

        Vector3 direction = _player.transform.position - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        this.transform.rotation = Quaternion.Euler(0f, 0f, angle - 90);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = (this.transform.up).normalized * speed * Time.deltaTime * 50;
        
        if (Physics2D.OverlapCircleAll(transform.position, 5, LayerMask.GetMask("Player")).Length > 0 && !plantedMine)
            PlantMine();

        if (Vector2.Distance(transform.position, _player.gameObject.transform.position) > 200)
            Destroy(this.gameObject);
    }

    private void PlantMine()
    {
        Instantiate(minePrefab, transform.position, Quaternion.identity);
        plantedMine = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<Player>())
        {
            _player.TakeDamage(20);
        }
    }
}
