using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    private const int MAX_CUBES = 70;
    [SerializeField] private GameObject cube;
    private List<Color> cubeColors = new List<Color>() { Color.red, Color.blue, Color.yellow, Color.magenta };

    [SerializeField] private GameObject WorldBounds;
    private Bounds bounds;

    void Start()
    {
        bounds = WorldBounds.GetComponent<PolygonCollider2D>().bounds;

        int colorChanger = 0;
        for (int i = 0; i < MAX_CUBES; ++i)
        {
            GameObject instance = Instantiate(cube, new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)), 
            this.transform.rotation,
            this.transform);
            instance.GetComponentInChildren<SpriteRenderer>().color = cubeColors[colorChanger];
            colorChanger++;

            if(colorChanger >= cubeColors.Count)
                colorChanger = 0;
        }
    }

    public void SpawnCube(Color color)
    {
        if (cubeColors.Contains(color)){
            GameObject instance = Instantiate(cube, new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)), this.transform.rotation);
            instance.GetComponent<SpriteRenderer>().color = color;
        }
    }
}
