using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAnimatorController : MonoBehaviour
{

    NavMeshAgent agent;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        this.agent = GetComponent<NavMeshAgent>();
        this.animator = GetComponentInChildren<Animator>();
        PlayerController.player.onSpellCastedCallback += onCast;
    }

    // Update is called once per frame
    void Update()
    {
        float speedness = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("speedness", speedness,.1f, Time.deltaTime);
        
    }

    public void onCast()
    {
        animator.SetTrigger("Cast");
    }
}
