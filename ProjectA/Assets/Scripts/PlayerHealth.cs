using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private Animator anim;
    public PlayerMovement movement;

    public bool dead = false;
    public int maxHealth = 3;
    public int currentHealth;
    public float damageInvulnLength;
    public float damageInvulnTime;
    // Start is called before the first frame update
    void Start()
    {
        
        damageInvulnTime = damageInvulnLength;
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;   
    }

    // Update is called once per frame
    void Update()
    {
        damageInvulnTime -= Time.deltaTime;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        damageInvulnTime = damageInvulnLength;
        if(currentHealth > 0){
        anim.SetTrigger("hurt");
        }
        if(currentHealth <= 0 && !dead)
        {
            dead = true;
            StartCoroutine(Die());  
        }
    }

    private IEnumerator Die()
    {
        anim.SetTrigger("dies");
        movement.moveSpeed = 0;
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }
}
