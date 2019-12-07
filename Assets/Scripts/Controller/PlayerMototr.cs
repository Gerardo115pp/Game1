using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMototr : MonoBehaviour
{
    // Start is called before the first frame update
    private NavMeshAgent agent;

    void Start()
    {
        this.agent = GetComponent<NavMeshAgent>();    
    }

    public void MoveToPoint(Vector3 point)
    {
        this.agent.SetDestination(point);
    }

    public bool IsStopped
    {
        get { return this.agent.isStopped; }
        set { 
            this.agent.isStopped = value;
            this.agent.updateRotation = !value;
        }
    }
}
