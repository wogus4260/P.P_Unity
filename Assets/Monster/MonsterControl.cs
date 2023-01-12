using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterControl : MonoBehaviour
{
    private Rigidbody2D rb2d;

    int nextMove;
    float moveSpeed;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        moveSpeed = 2.0f;

        Invoke("Think", 2);

    }

    void Update()
    {
        rb2d.velocity = new Vector2(nextMove * moveSpeed, rb2d.velocity.y);
    }

    void Think()
    {
        nextMove = Random.Range(-1, 1);
        rb2d.velocity = Vector2.up * 10.0f;
        Invoke("Think", 2); //스택 형식의 호출인지를 확인
    }
}

//collision.gameoject.getCompanent<monster>