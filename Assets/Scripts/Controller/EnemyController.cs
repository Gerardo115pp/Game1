using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyController : MonoBehaviour
{
    private float attack_speed = 1f,
                      attack_cooldown = 0f;
    private NavMeshAgent agent;
    private CharacterStats stats;
    private bool wasAttacked = false,
                      is_attacking = false;

    public float range_of_attack = 10f;
    public delegate void Attacked();
    public Attacked attackedCallback;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        stats = GetComponent<CharacterStats>();

        stats.onDamageTakenCallback += () => wasAttacked = true;
        stats.onAgonyCallback += this.syphone;
    }

    void Update()
    {
        if(PlayerController.player != null)
        {
            float distance = Vector3.Distance(PlayerController.player.Position, this.transform.position);
            attack_cooldown -= Time.deltaTime;
            if (distance <= range_of_attack || this.wasAttacked)
            {
                if(!is_attacking)
                {
                    GameStateController.stateController.battleStateHandler(GameStates.Battle);
                    this.is_attacking = true;
                }
                this.agent.SetDestination(PlayerController.player.Position);
                if (this.isTargetReachable())
                {
                    lookAtPoint(PlayerController.player.Position);
                    if (attack_cooldown <= 0f)
                    {
                        attackedCallback.Invoke();
                        PlayerController.player.TakeDamage = this.stats.damage.BaseValue;
                        this.attack_cooldown = 1f / this.attack_speed;
                    }
                }
            }
            else
            {
                if(this.is_attacking)
                {
                    GameStateController.stateController.battleStateHandler(GameStates.Playing);
                    this.is_attacking = false;
                }
            }
        }
    }

    private bool isTargetReachable()
    {
        return Vector3.Distance(PlayerController.player.Position, this.transform.position) < agent.stoppingDistance;
    }

    public void syphone()
    {
        PlayerController.player.heal(Convert.ToInt32(0.3f * PlayerController.player.MaxHealth));
        GameStateController.stateController.enemyKilled();
    }

    void lookAtPoint(Vector3 position)
    {
        Vector3 direction = (position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = lookRotation;
    }
}
