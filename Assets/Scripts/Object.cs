using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    ObjectSpawner objectSpawner;
    Scorekeeper scorekeeper;
    public int score = 10;
    Player player;

    private void OnTriggerEnter(Collider other)
    {
        objectSpawner = FindObjectOfType<ObjectSpawner>();


        if (other.tag == "Cube")
        {
            Spawn();
            Destroy(gameObject);
        }

        else if (other.tag == "Player")
        {
            player = FindObjectOfType<Player>();
            scorekeeper = FindObjectOfType<Scorekeeper>();

            //Color _color = gameObject.GetComponent<MeshRenderer>().material.color;
            //player.gameObject.GetComponentInChildren<MeshRenderer>().material.color = _color;
            Material material = GetComponent<MeshRenderer>().material;
            player.gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = material;

            if (this.gameObject.layer == 6 && player.lastObjID == 1 ||
                this.gameObject.layer == 7 && player.lastObjID == 2 ||
                this.gameObject.layer == 8 && player.lastObjID == 3)
            {
                score = score * (-2);               
            }

            scorekeeper.ModifyScore(score);
            Spawn();
            Destroy(gameObject);
        }

    }

    void Spawn()
    {
            objectSpawner.SpawnObjects();
    }
}
