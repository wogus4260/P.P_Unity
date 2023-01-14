using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterControl : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Collider2D col;
    private Animator ani;
    private SpriteRenderer sr;

    float movedistance;
    int selection;

    enum states{
        idle = 0,
        left = 1,
        right = 2
    }

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        ani = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        Invoke("Behaviour", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        rb2d.velocity = new Vector2(moveDistance, rb2d.velocity.y);
    }

    private void Behaviour(){
        switch(selection = Think()){
            case (int)states.idle:
                Debug.Log("IDLE");
                ani.SetInteger("Behaviour", (int)states.idle);
                break;
            case (int)states.left:
                Debug.Log("LEFT");
                sr.flipX = false;
                ani.SetInteger("Behaviour", (int)states.left);
                break;
            case (int)states.right:
                Debug.Log("RIGHT");
                sr.flipX = true;
                ani.SetInteger("Behaviour", (int)states.right);
                break;
        }
        Move();
        Invoke("Behaviour", 2f);
    }

    private int Think(){
        return Random.Range(0, 3);
    }

    private void Move(){
        if(selection == states.idle()) return;
        moveDistance = Random.Range(-1, 1);
        if(selection == states.right) moveDistance *= (-1);
    }
}
