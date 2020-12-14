using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityArcher : MonoBehaviour
{
    [SerializeField] private string projectileType;

    private SpriteRenderer rangeSpriteRenderer;

    private UnityMonster target;

    public UnityMonster Target
    {
        get { return target; }
    }
    private bool canAttack = true;
    private float attackTimer;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private int damage;
    public int Price { get; set; }

    public int Damage
    {
        get
        {
            return damage;
        }
    }

    public float ProjectileSpeed
    {
        get
        {
            return projectileSpeed;
        }
    }

    private Queue<UnityMonster> monsters = new Queue<UnityMonster>();
    // Start is called before the first frame update
    void Start()
    {
        rangeSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        Debug.Log(target);
    }
    protected virtual void Attack()
    {
        if (!canAttack)
        {
            attackTimer += Time.deltaTime;

            if (attackTimer >= attackCooldown)
            {
                canAttack = true;
                attackTimer = 0f;
            }
        }
        if (target == null && monsters.Count > 0)
        {
            target = monsters.Dequeue();
        }
        if (target != null)
        {
            if (canAttack)
            {
                Shoot();

                canAttack = false;
            }

        }
        
        else if (monsters.Count > 0)
        {
            target = monsters.Dequeue();
        }
        if (target != null && !target.isActive)
        {
            target = null;
        }
        
       
    }
    protected virtual void Shoot()
    {
        
        Projectile projectile = GameManager.Instance.Pool.GetObject(projectileType).GetComponent<Projectile>();
        projectile.transform.position = transform.position;

        projectile.Initialize(this);
    }


    public void Select()
    {
        rangeSpriteRenderer.enabled = !rangeSpriteRenderer.enabled;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Monster")
        {
            monsters.Enqueue(collision.GetComponent<UnityMonster>());
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Monster")
        {
            target = null;
        }
    }
}
