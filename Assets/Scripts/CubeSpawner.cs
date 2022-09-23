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
        var tileList = GameObject.FindGameObjectsWithTag("Tile");
        foreach (GameObject tile in tileList)
        {
            tiles.Add(tile);
            if(!tile.GetComponent<Tile>().availiable)
            {
                tiles.Remove(tile);
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

    void Spawn()
    {
        CheckTilesAvailiability();

        int tileIndex = Random.Range(0, tiles.Count);
        if (tiles[tileIndex].GetComponent<Tile>().isTaken || !(tiles[tileIndex].GetComponent<Tile>().availiable))
        {
            tiles.Remove(gameObject);
        }
        Instantiate(cube, tiles[tileIndex].transform.position + offset, Quaternion.identity);
    }

    void CheckTilesAvailiability()
    {
        var availiableList = GameObject.FindGameObjectsWithTag("Tile");
        foreach (GameObject _tile in availiableList)
        {
            if (_tile.GetComponent<Tile>().isTaken)
            {
                tiles.Remove(_tile);
            }
            if (!_tile.GetComponent<Tile>().availiable)
            {
                tiles.Remove(_tile);
            }
        }
    }
}
