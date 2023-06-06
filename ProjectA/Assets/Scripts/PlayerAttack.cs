using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;

    public Rigidbody2D rb; 
    public PlayerInputActions playerControls;


    public int damage = 1;

    public PlayerMovement Player;
    public LayerMask Enemies;
    public Vector2 attackRange;
    private Collider2D[] enemiesToDamage;

    private InputAction fire;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Awake()
    {
        playerControls = new PlayerInputActions();
    }
    
    private void OnEnable()
    {

        fire = playerControls.Player.Fire;
        fire.Enable();
        fire.performed += Fire;
    }

    private void OnDisable()
    {
        fire.Disable();
    }

    void Update()
    {
        
        
    }

    private void Fire(InputAction.CallbackContext context)
    {
        anim.SetTrigger("attack");
        if(Player.GetComponent<PlayerMovement>().isFacingRight)
        {
        enemiesToDamage = Physics2D.OverlapBoxAll(new Vector2(transform.position.x + 0.7f, transform.position.y + 0.69f),
        attackRange, 0, Enemies);
        }
        else
        {
        enemiesToDamage = Physics2D.OverlapBoxAll(new Vector2(transform.position.x - 0.7f, transform.position.y + 0.69f),
        attackRange, 0, Enemies);
        }
        for(int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<EnemyHealth>().TakeDamage(damage);
        }

    }

    void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        if(Player.GetComponent<PlayerMovement>().isFacingRight)
        {
        Gizmos.DrawWireCube(new Vector3(transform.position.x + 0.7f, transform.position.y + 0.69f, transform.position.z), attackRange);
        }
        else
        {
        Gizmos.DrawWireCube(new Vector3(transform.position.x - 0.7f, transform.position.y + 0.69f, transform.position.z), attackRange);
        }
    }

    
}
