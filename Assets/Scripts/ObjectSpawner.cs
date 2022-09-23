using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public List<GameObject> tiles = new List<GameObject>();
    [SerializeField] List<GameObject> objects = new List<GameObject>();
    Vector3 offset = new Vector3(0, 0.3f, 0);
    [SerializeField] int objectCount;

    private void Start()
    {
        var tileList = GameObject.FindGameObjectsWithTag("Tile");
        foreach (GameObject tile in tileList)
        {
            tiles.Add(tile);
        }

        for (int i = 0; i < objectCount; i++)
        {
            SpawnObjects();
        }
    }

    public void SpawnObjects()
    {
        int activeObj = 0;
        var objList = GameObject.FindGameObjectsWithTag("Object");
        foreach (GameObject obj in objList)
        {
            if (obj.activeInHierarchy)
            {
                activeObj++;
            }
        }
        if (activeObj / 2 > objectCount + 1)
        {
            return;
        }

        int tileIndex = Random.Range(0, tiles.Count);
        int objIndex = Random.Range(0, objects.Count);
        if (tiles[tileIndex].GetComponent<Tile>().isTaken || !(tiles[tileIndex].GetComponent<Tile>().availiable))
        {
            tiles.Remove(gameObject);
        }
        Instantiate(objects[objIndex], tiles[tileIndex].transform.position + offset, Quaternion.identity);
    }
}
