using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveMine : MonoBehaviour
{
    private Player _player;
    private Animator _animator;

    private float timeSincePlanted = 0;
    void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (timeSincePlanted > 5)
        {
            _animator.SetTrigger("Exploded");
            AudioManager.instance.PlaySound("MineExplosion");
            timeSincePlanted = -5;
        }
        timeSincePlanted += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            _player.TakeDamage(20);
            Vector3 direction = (_player.transform.position - transform.position).normalized;
            _player.GetComponent<Rigidbody2D>().AddForce(direction * 5, ForceMode2D.Impulse);
            AudioManager.instance.PlaySound("MineExplosion");
            _animator.SetTrigger("Exploded");
        }
    }

    public void MineExploded()
    {
        Destroy(gameObject);
    }
}
