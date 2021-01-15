using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kretanje : MonoBehaviour
{
    private Vector3 offset;

    public GameObject player;
    public GameObject center;
    public GameObject up;
    public GameObject down;
    public GameObject left;
    public GameObject right;

    public int step;
    public float speed = (float)0.01;
    bool input = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (input == true)
        {
            if (Input.GetKey(KeyCode.D))
            {
                StartCoroutine("moveUp");
                input = false;
            }

            if (Input.GetKey(KeyCode.A))
            {
                StartCoroutine("moveDown");
                input = false;
            }

            if (Input.GetKey(KeyCode.W))
            {
                StartCoroutine("moveLeft");
                input = false;
            }

            if (Input.GetKey(KeyCode.S))
            {
                StartCoroutine("moveRight");
                input = false;
            }
        }
    }

    IEnumerator moveUp()
    {
        for (int i = 0; i < (90 / step); i++)
        {
            player.transform.RotateAround(up.transform.position, Vector3.right, step);
            yield return new WaitForSeconds(speed);
        }
        center.transform.position = player.transform.position;
        input = true;
    }

    IEnumerator moveDown()
    {
        for (int i = 0; i < (90 / step); i++)
        {
            player.transform.RotateAround(down.transform.position, Vector3.left, step);
            yield return new WaitForSeconds(speed);
        }
        center.transform.position = player.transform.position;
        input = true;
    }

    IEnumerator moveLeft()
    {
        for (int i = 0; i < (90 / step); i++)
        {
            player.transform.RotateAround(left.transform.position, Vector3.forward, step);
            yield return new WaitForSeconds(speed);
        }
        center.transform.position = player.transform.position;
        input = true;
    }

    IEnumerator moveRight()
    {
        for (int i = 0; i < (90 / step); i++)
        {
            player.transform.RotateAround(right.transform.position, Vector3.back, step);
            yield return new WaitForSeconds(speed);
        }
        center.transform.position = player.transform.position;
        input = true;
    }
}
