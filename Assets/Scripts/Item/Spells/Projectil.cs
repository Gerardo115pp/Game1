using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Projectil : MonoBehaviour
{
    public float speed;
    protected int frame_limit;
    private int ttl;
    private CapsuleCollider capsuleCollider;
    public Spell spell;

    void Awake()
    {
        frame_limit = 900;
        this.capsuleCollider = GetComponent<CapsuleCollider>();
    }
    // Update is called once per frame
    void Update()
    {
        this.transform.position += (this.transform.forward * Time.deltaTime) * this.speed;
        this.ttl++;
        if(ttl >= frame_limit && frame_limit != -1)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.name != "Player")
        {
            if(other.tag == "Enemy")
            {
                spell.affectTarget(other);
            }
            Destroy(gameObject);
        }
    }

    public int TTL
    {
        get { return this.ttl; }
    }
}
