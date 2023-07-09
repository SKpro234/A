using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatBehavior : MonoBehaviour
{
    private Animator anim;

    public float moveSpeed = 1f;
    Rigidbody2D rb;
    public BoxCollider2D bc;
    public PlayerMovement playerMovement;
    
    GameObject player;
    
    public float KnockBackLength = 0.1f;
    public float KnockBackSpeed = 8f;
    public bool isKnockBack;


    // Start is called before the first frame update
    void Start()
    {
        isKnockBack = false;
        //anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerMovement = player.GetComponent<PlayerMovement>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!isKnockBack){
            if(gameObject.transform.position.x - playerMovement.Pposition.x < 0)
            {
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
                //anim.SetBool("iswalking", true);
            }
            else
            {
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
                //anim.SetBool("iswalking", true);
            }
        }
    
    }

    public void KnockBack()
    {
        isKnockBack = true;
        StartCoroutine(kb());
    }

    private IEnumerator kb()
    {
        
        if(playerMovement.isFacingRight)
        {
            rb.velocity = new Vector2(KnockBackSpeed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-KnockBackSpeed, 0);
        }
        yield return new WaitForSeconds(KnockBackLength);
        isKnockBack = false;
    }


    private bool isFacingLeft()
    {
        return transform.localScale.x < Mathf.Epsilon;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 6) //6 = terrain layer
        {
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
        else
        {
            Debug.Log("bruh");
        }
    }
}
