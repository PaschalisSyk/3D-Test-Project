using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public List<GameObject> tiles = new List<GameObject>();
    [SerializeField] GameObject cube;
    Vector3 offset = new Vector3(0, 0.5f, 0);
    Player player;
    [SerializeField] int cubesCount;
    [SerializeField] int cubesSpawnTime;

    private void Start()
    {
        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Tile"))
        {
            tiles.Add(gameObject);
            if(!gameObject.GetComponent<Tile>().availiable)
            {
                tiles.Remove(gameObject);
            }
        }

        player = FindObjectOfType<Player>();

        StartCoroutine(SpawnCubes());
    }

    IEnumerator SpawnCubes()
    {
        while(!FindObjectOfType<Player>()._lose)
        {
            yield return new WaitForSeconds(cubesSpawnTime);
            if(!player.canMove)
            {
                Spawn();
            }
        }
        
    }

    private void Update()
    {
        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Tile"))
        {
            if (gameObject.GetComponent<Tile>().isTaken)
            {
                tiles.Remove(gameObject);
            }
            if (!gameObject.GetComponent<Tile>().availiable)
            {
                tiles.Remove(gameObject);
            }
        }
    }

    void Spawn()
    {
        int tileIndex = Random.Range(0, tiles.Count);
        if (tiles[tileIndex].GetComponent<Tile>().isTaken || !(tiles[tileIndex].GetComponent<Tile>().availiable))
        {
            tiles.Remove(gameObject);
        }
        Instantiate(cube, tiles[tileIndex].transform.position + offset, Quaternion.identity);
    }
}
