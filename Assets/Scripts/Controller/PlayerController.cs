using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PlayerMototr))]
public class PlayerController : MonoBehaviour
{

    private Camera cam;
    private PlayerMototr mototr;
    private PlayerStats player_stats;

    public static PlayerController player;
    public LayerMask movement_mask;
    public Caster caster;
    public delegate void SpellCasted();
    public SpellCasted onSpellCastedCallback;


    public Vector3 Position
    {
        get { return this.transform.position; }
    }

    public int TakeDamage
    {
        set { player_stats.takeDamage(value); }
    }

    public float MaxHealth
    {
        get
        {
            return player_stats.MaxHealth;
        }
    }

    void Awake()
    {
        if (PlayerController.player != null)
        {
            Debug.LogError("Player already inicialized");
        }
        PlayerController.player = this;
        this.player_stats = GetComponent<PlayerStats>();
    }

    void Start()
    {
        cam = Camera.main;
        this.mototr = GetComponent<PlayerMototr>();
    }

    void Update()
    {
        this.handleMouseInput();
        this.handleSpellsChangeInput();
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            GameStateController.stateController.autoWin();
        }

    }

    private void handleSpellsChangeInput()
    {
        if(Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.V))
        {
            int spell_index = Input.GetKeyDown(KeyCode.C) ? 0 : 1;
            Item new_spell = Inventory.inventory.getItem(spell_index);
            if( new_spell != null )
            {
                EquipmentManager.equipmentManager.EquipedSpell = (Spell)new_spell;
            }
        }
    }

    private void handleMouseInput()
    {
        /*
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        */

        if (Input.GetMouseButtonDown(1) || Input.GetMouseButton(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Input.GetMouseButtonDown(1))
            {
                if (Physics.Raycast(ray, out hit, 100))
                {
                    this.mototr.IsStopped = true;
                    this.castSpell(hit.point);
                }
            }
            else if (Input.GetMouseButton(0))
            {
                if (Physics.Raycast(ray, out hit, 100))
                {
                    this.mototr.IsStopped = false;
                    mototr.MoveToPoint(hit.point);
                    Interactable interactable = hit.collider.GetComponent<Interactable>();
                    if (interactable != null)
                    {
                        interactable.OnFocused(transform);
                    }
                }

            }
        }
    }

    void lookAtPoint(Vector3 position)
    {
        Vector3 direction = (position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = lookRotation; 
    }

    void castSpell(Vector3 position)
    {
        Spell spell = EquipmentManager.equipmentManager.EquipedSpell;
        if (spell != null)
        {
            lookAtPoint(position);
            onSpellCastedCallback.Invoke();
            spell.cast(position);
        }
        
    }

    public void heal(int amount)
    {
        this.player_stats.healPlayer(amount);
    }


}
