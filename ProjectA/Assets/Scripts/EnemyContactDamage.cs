using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContactDamage : MonoBehaviour
{
    public int damage;
    public PlayerHealth playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {   

        if(collision.gameObject.tag == "Player" && playerHealth.damageInvulnTime <= 0)
        {
            playerHealth.TakeDamage(damage);
        }
    }
}
