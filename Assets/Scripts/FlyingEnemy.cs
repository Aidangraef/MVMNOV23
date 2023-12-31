using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    Transform player;
    float followDistance = 12f;
    float speed = 150f;
    Rigidbody2D rb;
    Vector3 startingPosition;
    Vector2 moveDirection;

    [SerializeField] float kbTime = 30f;
    float kbTimer = 0f;
    [SerializeField] float kbAmount = 20f;
    int maxHP = 3;
    int hp = 3;


    private void Awake()
    {
        startingPosition = transform.position;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, player.position) < followDistance)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            moveDirection = direction;
        }
        else
        {
            Vector3 direction = (startingPosition - transform.position).normalized;
            moveDirection = direction;
        }
    }

    private void FixedUpdate()
    {
        if(kbTimer > 0)
        {
            kbTimer -= 1;
        }
        else
        {
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * speed * Time.fixedDeltaTime;
        }        
    }

    // check for player collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // make player do their stuff
            collision.gameObject.GetComponent<PlayerMovement>().TakeDamage(transform, 1);
            // enemy do your stuff (recoil)
            //Knockback();
            TakeDamage(collision.gameObject.GetComponent<PlayerCombat>().attackDamage);
        }
    }

    public void TakeDamage(int amount)
    {
        //take damage
        hp -= amount;
        
        //take knockback
        Knockback();

        //if dead, die
        if (hp <= 0)
        {
            AkSoundEngine.PostEvent("flyerDie", this.gameObject);
            Destroy(gameObject);
        }
        //else, flash red+hurt
        else
        {
            AkSoundEngine.PostEvent("flyerHurt", this.gameObject);
            StartCoroutine(DoFlashRed());
        }
        }

        void Knockback()
    {
        kbTimer = kbTime;
        //StartCoroutine(ToggleScript());
        Debug.Log("Taking KB");
        Vector3 direction = transform.position - player.transform.position;
        rb.velocity = Vector2.zero;
        rb.velocity = direction.normalized * kbAmount;
    }

    IEnumerator DoFlashRed()
    {
        // initial setup
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        Color originalColor = new Color(1, 1, 1, 1);
        Color redColor = new Color(1, 0, 0, 1);

        // flash and wait and turn back
        for(int i = 0; i < 3; i++)
        {
            sprite.color = redColor;
            yield return new WaitForSeconds(0.1f);
            sprite.color = originalColor;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
