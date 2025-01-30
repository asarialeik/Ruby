using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    bool broken = true;
    Animator animator;
    public float changeTime = 3.0f;
    float verticalTimer;
    float horizontalTimer;
    int direction = 1;
    private float speed = 1.0f;
    public bool vertical = true;
    Rigidbody2D rigidbody2d;
    public AudioSource audio;
    public AudioSource hit;
    public AudioSource questComplete;
    public ParticleSystem smokeEffect;
    public EnemiesContainer enemiesContainer;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource> ();
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        verticalTimer = changeTime;
        horizontalTimer = changeTime;
        enemiesContainer.AddEnemy();
    }

    void Update()
    {
        if (vertical == true)
        {
            verticalTimer -= Time.deltaTime;
        }
        else if (vertical == false)
        {
            horizontalTimer -= Time.deltaTime;
        }

        if (verticalTimer < 0 && vertical == true)
        {
            direction = -direction;
            verticalTimer = changeTime;
            vertical = false;
        }

        if (horizontalTimer < 0 && vertical == false)
        {
            vertical = true;
            horizontalTimer = changeTime;
        }
    }

    void FixedUpdate()
    {
        if (!broken)
        {
            return;
        }
        Vector2 position = rigidbody2d.position;
        if (vertical)
        {
            position.y = position.y + speed * direction * Time.deltaTime;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }
        else
        {
            position.x = position.x + speed * direction * Time.deltaTime;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }

        rigidbody2d.MovePosition(position);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }

    public void Fix()
    {
        smokeEffect.Stop();
        broken = false;
        rigidbody2d.simulated = false;
        animator.SetTrigger("Fixed");
        audio.Stop();
        hit.Play();
        questComplete.Play();
        enemiesContainer.RemoveEnemy();
    }
}
