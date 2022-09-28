using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isTaken;
    public bool availiable = true;
    CubeSpawner cubeSpawner;
    ObjectSpawner objectSpawner;

    private void Awake()
    {
        cubeSpawner = FindObjectOfType<CubeSpawner>();
        objectSpawner = FindObjectOfType<ObjectSpawner>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Cube")
        {
            isTaken = true;
            availiable = false;
        }
        if (other.gameObject.tag == "Object" || other.gameObject.tag == "Player")
        {
            availiable = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Invoke("ResetTile", 1f);
        print("Exit");       
    }

    void ResetTile()
    {
        availiable = true;
        if (!cubeSpawner.tiles.Contains(gameObject))
        {
            cubeSpawner.tiles.Add(gameObject);
        }
        if (!objectSpawner.tiles.Contains(gameObject))
        {
            objectSpawner.tiles.Add(gameObject);
        }
    }
}
