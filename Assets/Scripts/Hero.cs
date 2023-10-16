using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum  States
{
    idle,
    run,
    jump
}

public class Hero : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private int lives = 5;
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private ContactFilter2D _platform;
    [SerializeField] private Animator animator;

    private Rigidbody2D _riginbody;
    private SpriteRenderer sprite;

    private States State
    {
        get => (States)animator.GetInteger("State");
        set => animator.SetInteger("State", (int)value);
    }

    public static Hero Instance { get; set; }

    private bool isGrounded => _riginbody.IsTouching(_platform);
   

    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        _riginbody = GetComponentInChildren<Rigidbody2D>();
        Instance = this;
    }

    public void GetDamage()
    {
        lives--;
        Debug.Log("Lives is: " + lives.ToString());
        if(lives == 0) Dead();
    }

    private void Dead()
    {
        //Destroy(gameObject);
        ScenesManager.StaticStartMenuScene();
    }

    private void Run()
    {
        if (isGrounded && State != States.run && State != States.jump) State = States.run;
        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
        sprite.flipX = dir.x < 0.0f;
    }

    private void Jump()
    {
        //State = States.jump;
        _riginbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    void Update()
    {
        //Debug.Log("Lives is: " + lives.ToString());
        if (isGrounded && State != States.idle) State = States.idle;
        if (Input.GetButton("Horizontal"))
            Run(); 
        if (isGrounded && Input.GetButtonDown("Jump"))
            Jump();
        else
        {
            if (!isGrounded && State != States.jump) State = States.jump;
        }
    }

    
}
