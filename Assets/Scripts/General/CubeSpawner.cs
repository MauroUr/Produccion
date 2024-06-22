using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    private const int MAX_CUBES = 70;
    [SerializeField] private GameObject cube;
    public List<Color> cubeColors;

    /*
     Level2: 040a84 - 2FB7BD - 49295d - f2ede4

     */
    [SerializeField] private GameObject WorldBounds;
    private Bounds bounds;

    [SerializeField] private GameObject Spawner;

    [SerializeField] private bool isInLevel3 = false;

    private void Awake()
    {
        cubeColors = new List<Color>() { Color.cyan, Color.red, Color.magenta, Color.yellow };
    }
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

            if (isInLevel3)
            {
                Level3Cube script = instance.GetComponent<Level3Cube>();
                script.enabled = true;
                script.StartMoving();
            }
            if (colorChanger >= cubeColors.Count)
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

            if (isInLevel3)
            {
                Level3Cube script = instance.GetComponent<Level3Cube>();
                script.enabled = true;
                script.StartMoving();
            }
        }
    }
}
