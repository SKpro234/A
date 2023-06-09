using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private Animator anim;

    public bool dead = false;
    public int maxHealth = 3;
    public int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;   
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if(currentHealth <= 0 && !dead)
        {
            dead = true;
            StartCoroutine(Die());  
        }
    }

    private IEnumerator Die()
    {
        anim.SetTrigger("dies");
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }
}
