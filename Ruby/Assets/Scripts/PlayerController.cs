using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //Animations
    Animator animator;
    Vector2 moveDirection = new Vector2(1, 0);

    public InputAction talkAction;

    //Movement
    public InputAction MoveAction;
    Rigidbody2D rigidbody2d;
    Vector2 move;
    private float speed = 3.0f;

    //Health
    public int maxHealth = 5;
    public int health { get { return currentHealth; } }
    int currentHealth;
    private float timeInvincible = 1.0f;
    bool isInvincible;
    float damageCooldown;

    //Projectiles
    public GameObject projectilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        //Animations
        animator = GetComponent<Animator>();


        talkAction.Enable();


        //Movement
        MoveAction.Enable();
        rigidbody2d = GetComponent<Rigidbody2D>();

        //Health
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //Health
        if (isInvincible)
        {
            damageCooldown -= Time.deltaTime;
            if (damageCooldown < 0)
            {
                isInvincible = false;
            }
        }

        //Movement
        move = MoveAction.ReadValue<Vector2>();

        //Projectiles
        if (Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            moveDirection.Set(move.x, move.y);
            moveDirection.Normalize();
        }
        animator.SetFloat("Look X", moveDirection.x);
        animator.SetFloat("Look Y", moveDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if (Input.GetKeyDown(KeyCode.X))
        {
            FindFriend();
        }
    }

    void FixedUpdate()
    {
        Vector2 position = (Vector2)rigidbody2d.position + move * speed * Time.deltaTime;
        rigidbody2d.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
            {
                return;
            }


            animator.SetTrigger("Hit");

            isInvincible = true;
            damageCooldown = timeInvincible;
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);

        UIHandler.instance.SetHealthValue(currentHealth / (float)maxHealth);
    }

    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(moveDirection, 300);
        animator.SetTrigger("Launch");
    }

    void FindFriend()
    {
        RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, moveDirection, 1.5f, LayerMask.GetMask("NPC"));
        if (hit.collider != null)
        {

        }
    }

}
