using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private SpriteRenderer sr;
    private Animator ani;

    int healthPoint, manaPoint, staminaPoint;

    bool isGround, isAttack, isHit;

    float low, col, runPower, jumpPower, knockBackPower;

    enum states
    {
        idle = 0,
        run = 1,
        jump = 2,
        attack = 3
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        ani = GetComponent<Animator>();

        isGround = false;
        isAttack = false;
        isHit = false;

        runPower = 5.0f;
        jumpPower = 10.0f;
        knockBackPower = 1.0f;

        healthPoint = 5;
        manaPoint = 0;
        staminaPoint = 5;

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        Attack();
        Anime();
    }

    private void Move()
    {
        low = Input.GetAxisRaw("Horizontal");
        if (low != 0 && !isAttack && !isHit) transform.Translate(new Vector2(low * runPower * Time.deltaTime, 0));
    }

    private void Jump()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (isGround)
            {
                rb2d.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
                rb2d.velocity = Vector2.up * jumpPower;
            }
        }
    }

    private void Attack()
    {
        if (Input.GetKey(KeyCode.A))
        {
            if (!isHit && isGround) isAttack = true;
        }
        else isAttack = false;
    }

    private void Anime()
    {
        if (low != 0 && !isHit)
        {
            if (low < 0) sr.flipX = true;
            else if (low > 0) sr.flipX = false;
        }

        if (isAttack) ani.SetInteger("behaviour", (int)states.attack);
        else if (isGround)
        {
            if (low != 0) ani.SetInteger("behaviour", (int)states.run);
            else ani.SetInteger("behaviour", (int)states.idle);
        }
        else if(!isGround) ani.SetInteger("behaviour", (int)states.jump);

    }

    private void OnCollisionEnter2D(Collision2D collision) //collision?
    {
        //발판에 착지 시
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
            isHit = false;
            rb2d.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        }

        //몬스터에게 피격 시
        if (collision.gameObject.CompareTag("Monster"))
        {
            rb2d.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
            isHit = true;
            healthPoint--;
            Debug.Log(healthPoint);

            if (collision.transform.position.x >= transform.position.x) knockBackPower = -0.8f;
            else knockBackPower = 0.8f;
            rb2d.AddForce(transform.position * knockBackPower, ForceMode2D.Impulse);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        //발판에서 이탈 시
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = false;
            rb2d.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        }
    }
}
