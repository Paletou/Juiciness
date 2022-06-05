using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float Health;
    public float MaxSpeed;
    public float AccelerationRate;

    // Private Variables
    float Speed;
    float DriftFactor;
    GameObject Player;
    Vector2 PlayerDirection;
    Vector2 PreviousPlayerDirection;
    Rigidbody2D rb;
    BoxCollider2D col;

    private Color m_originalColor;
    private SpriteRenderer m_sprite;

    private Animator m_animator;
    
    
    private GameManager m_gameManager;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        Player = GameObject.FindWithTag("Player");
        DriftFactor = 1;
        m_gameManager = FindObjectOfType<GameManager>();
        m_sprite = GetComponent<SpriteRenderer>();
        m_animator = GetComponent<Animator>();

        m_originalColor = m_sprite.color;

    }

    void Update()
    {
        //Should I rotate towards Player ?
        PlayerDirection = Player.transform.position - transform.position;
        if(Mathf.Sign(PlayerDirection.x) != Mathf.Sign(PreviousPlayerDirection.x))
        {
            RotateTowardsPlayer();
        }
        PreviousPlayerDirection = PlayerDirection;

        //Go towards Player
        rb.velocity = new Vector2(transform.forward.z * DriftFactor * Speed * Time.fixedDeltaTime, rb.velocity.y);

        //Die
        if(Health <= 0)
        {
            Debug.Log("1");
            
            m_animator.SetTrigger("DeathTrigger");

            m_gameManager.addScore();
            
            Destroy(gameObject);
            
        }

        if(Speed <= 0)
        {
            StartCoroutine(GetToSpeed(MaxSpeed));
        }
        //Debug.Log(Speed);
    }

    public void GetDamage(float dmg)
    {
        Health -= dmg;

        StartCoroutine(ChangeColor());

    }

    void RotateTowardsPlayer()
    {
        if (PlayerDirection.x < 0)
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        else
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        DriftFactor = -1;
        StartCoroutine(GetToSpeed(0));
    }

    IEnumerator GetToSpeed( float s)
    {
        //Debug.Log(s);
        float baseSpeed = Speed;
        float SignMultiplier = Mathf.Sign(s - Speed);
        for(float f=baseSpeed; f*SignMultiplier<=s; f += AccelerationRate*SignMultiplier)
        {
            Speed = f;
            yield return null;
        }
        DriftFactor = 1;
    }
    
    IEnumerator ChangeColor()
    {
            
        m_sprite.color = Color.red;
            
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(0.1f);

        m_sprite.color = m_originalColor;
    }
    
    IEnumerator Death()
    {
        m_animator.SetTrigger("DeathTrigger");
        
            
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(0.1f);

        
    }

}
