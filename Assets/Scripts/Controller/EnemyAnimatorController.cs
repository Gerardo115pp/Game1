using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimatorController : MonoBehaviour
{

    private NavMeshAgent agent;
    private Animator animator;
    private EnemyController controller;

    // Start is called before the first frame update
    void Start()
    {
        this.agent = GetComponent<NavMeshAgent>();
        this.animator = GetComponentInChildren<Animator>();
        this.controller = GetComponent<EnemyController>();
        this.controller.attackedCallback += onAttackHappend;
    }

    // Update is called once per frame
    void Update()
    {
        float speedness = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("speedness", speedness, .1f, Time.deltaTime);
    }

    private void onAttackHappend()
    {
        this.animator.SetTrigger("Attack");
    }
}