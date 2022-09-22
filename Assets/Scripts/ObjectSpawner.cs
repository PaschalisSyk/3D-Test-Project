using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public List<GameObject> tiles = new List<GameObject>();
    [SerializeField] List<GameObject> objects = new List<GameObject>();
    Vector3 offset = new Vector3(0, 0.5f, 0);
    public int objectCount = 4;

    private void Start()
    {
        foreach(GameObject gameObject in GameObject.FindGameObjectsWithTag("Tile"))
        {
            tiles.Add(gameObject);
        }

        for (int i = 0; i < objectCount; i++)
        {
            SpawnObjects();
        }

    }

    public void SpawnObjects()
    {
        int activeObj =0;
        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Object"))
        {
            if(gameObject.activeInHierarchy)
            {
                activeObj++;
            }
        }
        if(activeObj/2 > objectCount)
        {
            return;
        }

        int tileIndex = Random.Range(0, tiles.Count);
        int objIndex = Random.Range(0, objects.Count);
        if(tiles[tileIndex].GetComponent<Tile>().isTaken || !(tiles[tileIndex].GetComponent<Tile>().availiable))
        {
            tiles.Remove(gameObject);
        }
        Instantiate(objects[objIndex], tiles[tileIndex].transform.position + offset, Quaternion.identity);
    }

    private void Update()
    {
        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Tile"))
        {
            /*if(gameObject.GetComponent<Tile>().isTaken)
            {
                tiles.Remove(gameObject);
            }*/
            if(gameObject.GetComponent<Tile>().availiable)
            {
                tiles.Add(gameObject);
            }
        }

    }
}
