using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private Animator animator;
    
    [SerializeField] Healthbar healthbar1, healthbar2, healthbar3;
    [SerializeField] Image[] lives;
    [SerializeField] int startingHealth, currentHealth;
    
    bool dead;

    private void Awake()
    {
        currentHealth = startingHealth;
        animator = GetComponent<Animator>();
    }
   /* private void Update()
    {
        LoseLife(1);
    }*/

    public void LoseLife(int damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);

        healthbarAnimation();
        StartCoroutine(waitThenload());


        if (currentHealth > 0)
        {
            animator.SetTrigger("hurt");
        }
        else
        {
            if(!dead)
            {
                animator.SetTrigger("Die");
                Debug.Log("Player lost");
                GetComponent<Player_Controller>().enabled = false;
                dead = true;
            }
            
        }
    }

    void healthbarAnimation()
    {
        if (currentHealth == 2)
        {
            healthbar1.playanimation();
        }
        else if (currentHealth == 1)
        {
            healthbar2.playanimation();
        }
        else if (currentHealth == 0)
        {
            healthbar3.playanimation();
        }
    }

    private IEnumerator waitThenload()
    {
        yield return new WaitForSecondsRealtime(1.0f);
        lives[currentHealth].enabled = false;
    }
}
