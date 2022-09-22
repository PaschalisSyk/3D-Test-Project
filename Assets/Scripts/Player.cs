using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector3 up = Vector3.zero,
    right = new Vector3(0, 90, 0),
    down = new Vector3(0, 180, 0),
    left = new Vector3(0, 270, 0),
    currentDirection = Vector3.zero;

    Vector3 nextPos, destination, direction;

    float speed = 2f;
    float raylenght = 1f;

    public bool canMove;
    [SerializeField] bool validMove = true;
    public bool _lose = false;
    public int lastObjID;
    void Start()
    {
        currentDirection = up;
        nextPos = Vector3.forward;
        destination = transform.position;
    }

    void Update()
    {
        Move();
        MoveCount();
    }

    void CheckForWalls(Vector3 _direction)
    {
        Ray myRay = new Ray(transform.position + new Vector3(0, 0.1f, 0), _direction);
        RaycastHit hit;

        Debug.DrawRay(myRay.origin, myRay.direction, Color.red);
        
        if(Physics.Raycast(myRay,out hit,raylenght))
        {
            if(hit.collider.tag == "Wall" || hit.collider.tag == "Cube")
            {
                validMove = false;
            }
        }
        else
        {
            validMove = true;
        }
    }


    void Move()
    {
        StartCoroutine(MoveDelay());

        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.W))
        {
            CheckForWalls(transform.forward);
            nextPos = Vector3.forward;
            currentDirection = up;
            canMove = true;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            CheckForWalls(-transform.forward);
            nextPos = Vector3.back;
            currentDirection = down;
            canMove = true;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            CheckForWalls(transform.right);
            nextPos = Vector3.right;
            currentDirection = right;
            canMove = true;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            CheckForWalls(-transform.right);
            nextPos = Vector3.left;
            currentDirection = left;
            canMove = true;
        }

        if(Vector3.Distance(destination,transform.position) <= 0.0001f)
        {
            if(canMove && validMove)
            {
                destination = transform.position + nextPos;
                direction = nextPos;
                canMove = false;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Object")
        {
            if(other.gameObject.layer == 6)
            {
                Invoke("IDOne", 1);
            }
            if(other.gameObject.layer == 7)
            {
                Invoke("IDTwo", 1);
            }

        }
        if(other.gameObject.tag == "Tiles")
        {
            other.GetComponent<Tile>().availiable = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        other.GetComponent<Tile>().availiable = true;
    }

    int CheckForMoves(Vector3 _direction)
    {
        
        Ray myRay = new Ray(transform.position + new Vector3(0, 0.1f, 0), _direction);
        RaycastHit hit;

        Debug.DrawRay(myRay.origin, myRay.direction, Color.red);

        if (Physics.Raycast(myRay, out hit, raylenght))
        {
            if (hit.collider.tag == "Wall" || hit.collider.tag == "Cube")
            {
                return -1;
            }
            return 0;
        }
        else
        {
            return 0;
        }
    }

    void MoveCount()
    {
        int count = CheckForMoves(transform.forward)
            + CheckForMoves(transform.right)
            + CheckForMoves(-transform.forward)
            + CheckForMoves(-transform.right);

        if(count == -4)
        {
            _lose = true;
        }
    }

    IEnumerator MoveDelay()
    {
        yield return new WaitForSeconds(1f);

    }

    void IDOne()
    {
        lastObjID = 1;
    }
    void IDTwo()
    {
        lastObjID = 2;
    }
}
