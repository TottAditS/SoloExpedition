using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    private float horizontal;
    [Range(0f,20f)] public float jumppower;
    [Range(0f, 20f)] public float speed;
    private bool isfacingright = true;
    public Animator animator;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundcek;
    [SerializeField] private LayerMask groundlayer;

    public GameObject attackpoint;
    [Range(0f,10f)] public float radius;
    [Range(0f, 100f)] public float atkdamage;
    public LayerMask enemies;

    public float health;
    [Range(1f, 100f)] public float maxhealth;

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        animator.SetFloat("speed", Mathf.Abs(horizontal));

        flip();

        if (Input.GetKeyDown(KeyCode.Space) && isground())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumppower);
            jumps();
        }

        if (Input.GetKeyDown(KeyCode.Space) && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            jumps();
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            attacks();
        }

        if (health <= 0)
        {
            Debug.Log("you ded");
        }

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool isground()
    {
        return Physics2D.OverlapCircle(groundcek.position, 0.2f, groundlayer);
    }

    public bool Checkground()
    {
        if (isground())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void flip()
    {
        if (isfacingright && horizontal < 0f || !isfacingright && horizontal > 0f)
        {
            isfacingright = !isfacingright;
            Vector3 localscale = transform.localScale;
            localscale.x *= -1f;
            transform.localScale = localscale;
        }
    }

    public void attacks()
    {
        if (!animator.GetBool("isattack"))
        {
            animator.SetBool("isattack", true);
            givedamage();
        }
    }
    public void atkdone()
    {
        animator.SetBool("isattack", false);
    }
    public void jumps()
    {
        if (!animator.GetBool("isjump"))
        {
            animator.SetBool("isjump", true);
        }
    }
    public void jumpdone()
    {
        animator.SetBool("isjump", false);
        animator.SetBool("isfall", true);
    }
    public void fallings()
    {
        bool touch = Checkground();

        if (touch)
        {
            animator.SetBool("isfall", false);
        }
    }

    public void givedamage()
    {
        Collider2D[] enemy = Physics2D.OverlapCircleAll(attackpoint.transform.position, radius, enemies);

        foreach (Collider2D enemyobject in enemy)
        {
            Debug.Log("hit enemy");
            enemyobject.GetComponent<EnemyScript>().health -= atkdamage;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackpoint.transform.position, radius);
    }
}
