using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class EnemyScript : MonoBehaviour
{
    public Transform player;
    public Transform enemy;
    public GameObject attackpoint;
    public GameObject detectpoint;
    public bool isfacingright = false;

    [Range(0f, 20f)] public float detectradius;
    [Range(0f,20f)] public float speed;
    private Vector2 destination;
    public Animator animator;
    public LayerMask lplayer;
    private float movingspeed;

    public float health;
    [Range(1f,100f)] public float maxhealth;
    [Range(0f, 10f)] public float atkradius;
    [Range(0f, 10f)] public float atkdamage;


    void Start()
    {
        health = maxhealth;
    }

    void Update()
    {
        float distances = Vector3.Distance(enemy.position, player.position);

        flip();

        if (distances <= detectradius) 
        {
            movetoplayer();
            movingspeed = 1;
            animator.SetFloat("speed", movingspeed);
        }
        else
        {
            movingspeed = -1;
            animator.SetFloat("speed", movingspeed);
        }

        if (health <= 0)
        {
            imded();
        }
    }

    public void movetoplayer()
    {
        destination = new Vector2(player.transform.position.x, player.transform.position.y);
        transform.position = Vector2.Lerp(enemy.transform.position, destination, speed * Time.deltaTime);
    }

    public void imded()
    {
        Destroy(gameObject);
    }

    public void enemyattacks()
    {
        if (!animator.GetBool("isattack"))
        {


            animator.SetBool("isattack", true);
            enemydamage();
        }
    }
    public void atkdone()
    {
        animator.SetBool("isattack", false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enemyattacks();
        }
    }

    public void enemydamage()
    {
        Collider2D[] players = Physics2D.OverlapCircleAll(attackpoint.transform.position, atkradius, lplayer);

        foreach (Collider2D playersobject in players)
        {
            playersobject.GetComponent<Movement>().health -= atkdamage;
        }
    }

    private void flip()
    {
        if (!isfacingright && movingspeed < 0f || isfacingright && movingspeed > 0f)
        {
            isfacingright = !isfacingright;
            Vector3 localscale = transform.localScale;
            localscale.x *= -1f;
            transform.localScale = localscale;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(detectpoint.transform.position, detectradius);
        Gizmos.DrawWireSphere(attackpoint.transform.position, atkradius);
    }
}
