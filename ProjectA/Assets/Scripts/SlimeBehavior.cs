using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBehavior : MonoBehaviour
{
    private Animator anim;

    [SerializeField] private float moveSpeed = 5f;
    
    Rigidbody2D rb;
    public BoxCollider2D bc;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if(isFacingLeft())
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            anim.SetBool("iswalking", true);
        }
        else
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            anim.SetBool("iswalking", true);
        }
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
