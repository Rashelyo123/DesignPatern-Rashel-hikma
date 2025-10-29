using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSection : MonoBehaviour
{

    public float Speed = -7;
    bool IsMove = true;
    void Update()
    {
        if (IsMove)
        {
            transform.position += new Vector3(0, 0, Speed) * Time.deltaTime;

        }
    }

    public void NotMove()
    {
        IsMove = false;
    }
    public void Move()
    {
        IsMove = true;
    }
}
