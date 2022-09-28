using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public List<GameObject> tiles = new List<GameObject>();
    [SerializeField] List<GameObject> objects = new List<GameObject>();
    Vector3 offset = new Vector3(0, 0.3f, 0);
    [SerializeField] int objectCount;
    public bool canSpawnObj = true;

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

        for (int i = 0; i < objectCount; i++)
        {

            SpawnObjects();
        }
    }

    public void SpawnObjects()
    {
            canSpawnObj = false;
            CheckTilesAvailiabilityForObj();
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
            Instantiate(objects[objIndex], tiles[tileIndex].transform.position + offset, Quaternion.identity);
            tiles.Remove(tiles[tileIndex]);
            canSpawnObj = true;
    }

    void CheckTilesAvailiabilityForObj()
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

    IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(0.5f);
        canSpawnObj = true;
    }

}
