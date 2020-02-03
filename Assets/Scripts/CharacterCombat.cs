using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    private CharacterStats myStats;
    CharacterStats opponentStats;

    public float attackSpeed = 1f;
    private float attackCooldown = 0f;

    const float combatCooldown = 5;
    float lastAttackTime;
    public bool InCombat {get; private set;}

    public float attackDelay = .6f;

    public event System.Action OnAttack; //delegate with return type void no args

    void Start ()
    {
        myStats = GetComponent<CharacterStats>();
    }

    void Update ()
    {
        attackCooldown -= Time.deltaTime;

        if(Time.time - lastAttackTime > combatCooldown)
        {
            //no longer in combat
            InCombat = false;
        }
    }

    public void Attack (CharacterStats targetStats)
    {
        if (attackCooldown <= 0f)
        {
            //targetStats.TakeDamage(myStats.damage.GetValue());
            //StartCoroutine(DoDamge(targetStats, attackDelay));
            opponentStats = targetStats;
            if (OnAttack != null)
            {
                OnAttack();
            }

            attackCooldown = 1f/attackSpeed;
            InCombat = true;
            lastAttackTime = Time.time;
        }
    }

    // IEnumerator DoDamge (CharacterStats stats, float delay)
    // {
    //     yield return new WaitForSeconds(delay);



    // }

    public void AttackHit_AnimationEvent()
    {
        opponentStats.TakeDamage(myStats.damage.GetValue());
        if (opponentStats.currentHealth <= 0)
        {
            InCombat = false;
        }
    }
}
