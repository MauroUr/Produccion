using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public CubeSpawner spawner;
    void Start()
    {
        if (this.GetComponent<BoxCollider2D>().IsTouchingLayers(3)) {
            spawner.SpawnCube(this.GetComponent<SpriteRenderer>().color);
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null && collision.gameObject.layer == 7)
        {
            spawner.SpawnCube(this.GetComponent<SpriteRenderer>().color);
            Destroy(this.gameObject);
        }
    }
}
