using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] protected Camera cam;
    [SerializeField] private List<GameObject> prefabsToSpawn;
    [SerializeField] private List<float> spawnTime;

    private List<float> waves = new List<float>() { 20f, 40f };
    protected Bounds bounds;
    private enum spawnFrom { top, bottom, left, right };

    private List<float> timeSinceSpawned;

    private void Start()
    {
        timeSinceSpawned = new List<float>();

        GameController.Instance.planetRecovered.AddListener(HalveSpawnRate);
        GameController.Instance.planetUnrecovered.AddListener(ReturnToNormal);

        for (int i = 0; i < prefabsToSpawn.Count; i++)
        {
            timeSinceSpawned.Add(-2);
        }
    }
    void Update()
    {
        bounds = GetCameraBounds(this.cam);

        for (int i = 0; i < prefabsToSpawn.Count; i++)
        {
            if (timeSinceSpawned[i] > spawnTime[i])
            {
                Spawn(prefabsToSpawn[i], this.transform);
                timeSinceSpawned[i] = 0;
            }
        }

        for (int i = 0; i < timeSinceSpawned.Count; i++)
            timeSinceSpawned[i] += Time.deltaTime;
    }
    private Bounds GetCameraBounds(Camera cam)
    {
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = cam.orthographicSize * 2;
        Bounds bounds = new Bounds(
            cam.transform.position,
            new Vector3(cameraHeight * screenAspect, cameraHeight, 0));
        return bounds;
    }

    private void HalveSpawnRate()
    {
        for (int i = 0; i < spawnTime.Count ; i++)
        {
            spawnTime[i] = spawnTime[i]/2;
        }
    }
    private void ReturnToNormal()
    {
        for (int i = 0; i < spawnTime.Count; i++)
        {
            spawnTime[i] = spawnTime[i] * 2;
        }
    }
    private void Spawn(GameObject prefab, Transform parent)
    {
        spawnFrom spawnLocation = (spawnFrom)Random.Range(0, 3);
        Vector3 spawnPosition = new Vector3();

        switch (spawnLocation)
        {
            case spawnFrom.top:
                spawnPosition = new Vector3(
                    Random.Range(bounds.min.x, bounds.max.x),
                    bounds.max.y + prefab.GetComponent<SpriteRenderer>().bounds.size.y,
                    this.transform.position.z);
                break;

            case spawnFrom.bottom:
                spawnPosition = new Vector3(
                    Random.Range(bounds.min.x, bounds.max.x),
                    bounds.min.y - prefab.GetComponent<SpriteRenderer>().bounds.size.y,
                    this.transform.position.z);
                break;

            case spawnFrom.left:
                spawnPosition = new Vector3(
                    bounds.min.x - prefab.GetComponent<SpriteRenderer>().bounds.size.x,
                    Random.Range(bounds.min.y, bounds.max.y),
                    this.transform.position.z);
                break;

            case spawnFrom.right:
                spawnPosition = new Vector3(
                    bounds.max.x + prefab.GetComponent<SpriteRenderer>().bounds.size.x,
                    Random.Range(bounds.min.y, bounds.max.y),
                    this.transform.position.z);
                break;
        }

        Instantiate(prefab, spawnPosition, Quaternion.identity, parent);
    }
    public GameObject SpawnCube(GameObject prefab, Transform parent)
    {
        Vector3 spawnPosition = new Vector3(
                    Random.Range(bounds.min.x, bounds.max.x),
                    bounds.max.y + prefab.GetComponent<SpriteRenderer>().bounds.size.y,
                    this.transform.position.z);

        return Instantiate(prefab, spawnPosition, Quaternion.identity, parent);
    }
}
