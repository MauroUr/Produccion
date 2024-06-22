using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Cube : MonoBehaviour
{
    private Vector3 direction;
    private Rigidbody2D rb;
    private List<Color> cubeColors = new List<Color>() { Color.red, Color.cyan, Color.yellow, Color.magenta };
    public void StartMoving()
    {
        Vector3[] directions = { this.transform.right, -this.transform.right, this.transform.up, -this.transform.up };
        direction = directions[Random.Range(0, directions.Length)];
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        rb.velocity = direction * Time.deltaTime * 300;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && (collision.gameObject.layer == 3))
        {
            direction *= -1;
            this.GetComponent<SpriteRenderer>().color = cubeColors[Random.Range(0, cubeColors.Count)];
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null && (collision.gameObject.layer == 3))
        {
            direction *= -1;
            this.GetComponent<SpriteRenderer>().color = cubeColors[Random.Range(0, cubeColors.Count)];
        }
    }
}
