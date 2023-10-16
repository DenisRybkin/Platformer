using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingWood : Wood
{
    private float speed = 2f;
    private Vector3 dir;
    private SpriteRenderer sprite;
    //private Rigidbody2D riginbody;

    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private ContactFilter2D _platform;

    private int currentWaypointIndex = 0;

    private void Start()
    {
        sprite.flipX = !sprite.flipX;
        dir = transform.right;
    }

    protected void Update()
    {
        Move();
    }

    protected void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
      
    }

    private void Move()
    {
        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < 1.16)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length) currentWaypointIndex = 0;
            sprite.flipX = !sprite.flipX;
        }
        transform.position = Vector2.MoveTowards(
                transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed
            );


        /*Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + transform.up * 0.1F + transform.right * dir.x * 0.7F, 0.01F);

        if (colliders.Length > 0) dir *= -1F;

        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, Time.deltaTime);*/
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Hero")
        {
            Hero hero = collision.gameObject.GetComponent<Hero>();
            hero.GetDamage();
        }
    }
}
