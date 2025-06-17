using System;
using System.Collections;
using Globals;
using MathsAndSome;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using MasterCombatController;
using EnemyState;


namespace EnemyState
{
    public enum EState
    {
        seeking = 0, // Idle, checking for the player
        hunting = 1, // Actively chasing the player
        attacking = 2, // In attack range and attacking
        hooked = 3, // Has been hooked
        dead = 4    // Health <= 0
    }
}


[RequireComponent(typeof(AudioSource))]
public abstract class BaseEnemy : MonoBehaviour
{

    #region Variables
    [Header("Base Enemy")]
    [Header("State")]
    public EState s = EState.seeking;
    public bool seeking;
    public bool hunting;
    public bool attacking;
    [Header("Stats")]
    [Header("Health")]

    public int health = 100;

    [Header("Attacking")]

    public float seekRange = 100f;
    public float attackRange = 10f;
    public float maxYRange = 10f;
    public int projLimit = 5;

    [Header("Damage")]
    public int damage;
    public float attackTime = 0.2f;
    public bool canTakeDamage = true;
    public int healthToGive = 30;

    public bool inAttackRange()
    {
        Vector3 pd = mas.player.PlayerDistance(player, gameObject);
        return pd.x <= attackRange &&
        pd.z <= attackRange &&
        pd.y <= maxYRange;
    }

    public bool inHuntRange()
    {
        Vector3 pd = mas.player.PlayerDistance(player, gameObject);
        return pd.x <= seekRange &&
        pd.z <= seekRange &&
        pd.y <= maxYRange;
    }


    // float kbForce = 10f;

    [HideInInspector] public Rigidbody rb;


    [Header("Temp material stuff")]

    public Material hurtMat;
    public Material defaultMat;

    [HideInInspector] public GameObject player;

    [SerializeField] float iframes = 0.1f;


    [Header("Audio")]

    public List<AudioClip> audios;
    CombatController cc;


    #endregion
    #region Abstract Functions
    public abstract IEnumerator Hunt();
    public abstract IEnumerator Attack();
    #endregion

    #region Coroutines
    public IEnumerator IFrames()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(iframes);
        canTakeDamage = true;
    }

    public IEnumerator ChangeMaterial()
    {
        if (GetComponent<MeshRenderer>() != null)
        {
            GetComponent<MeshRenderer>().material = hurtMat;
        }

        yield return new WaitForSeconds(0.2f);

        if (GetComponent<MeshRenderer>() != null)
        {
            GetComponent<MeshRenderer>().material = defaultMat;
        }
    }

    const float checkWidth = 0.3f;
    const float checkHeight = 0.8f;

    public bool grounded()
    {
        Vector3 scale = gameObject.transform.localScale;
        Vector3 pos = gameObject.transform.position;
        List<Collider> colliders = Physics.OverlapBox(
            new Vector3(
                pos.x,
                pos.y - scale.y / 2,
                pos.z
            ),
            new Vector3(
                scale.x * checkWidth,
                checkHeight,
                scale.z * checkWidth
            )
        ).ToList();

        var c = colliders;

        if (this is Archer)
        {
            for (int i = 0; i < colliders.Count; i++)
            {
                if (colliders[i].GetComponent<ProjectileController>() != null)
                {
                    c.RemoveAt(i);
                }
            }
        }

        c.Remove(GetComponent<CapsuleCollider>());

        if (c.Count() > 0)
        {
            return true;
        }
        return false;
    }

    // Was abstract but was the same over all enemies
    public IEnumerator Seek()
    {
        seeking = true;

        if (inHuntRange())
        {
            s = EState.hunting;
        }

        yield return new WaitForSeconds(0.1f);

        seeking = false;
    }

    IEnumerator ActionLoop()
    {
        // Debug.Log("ActionLoop");

        if (s == EState.seeking && !seeking)
        {
            StartCoroutine(Seek());
        }

        if (s == EState.attacking && !attacking)
        {
            StartCoroutine(Attack());
        }

        if (s == EState.hunting && !hunting)
        {
            StartCoroutine(Hunt());
        }

        yield return new WaitForSeconds(0.01f);

        StartCoroutine(ActionLoop());
    }
    #endregion

    #region Functions
    /*public void TakeDamage(int damage, Vector3 hit)
    {
        if (canTakeDamage)
        {
            health -= damage;
            GetComponent<AudioSource>().PlayOneShot(audios[0], 1);

            if (hit != Vector3.zero)
            {
               TakeKnockback(hit, cc.currentWeapon.damage);
            }

            else
            {
                Debug.Log("hit==null");
            }

            StartCoroutine(ChangeMaterial());


            if (health <= 0)
            {
                Die();
            }
            StartCoroutine(IFrames());
        }
    }*/

    public void Die(RayData killer)
    {
        if (s != EState.dead)
        {
            if (healthToGive != 0)
            {
                {
                    MCC.TakeDamage(mas.player.GetCombatController(), -healthToGive, Vector3.zero, null, null);
                }
            }

            if (killer.attackerName != AttackEnums.Attacker.silent)
            {
                GetComponent<AudioSource>().PlayOneShot(mas.player.GetCombatController().bowKill, 1);
            }
            Destroy(GetComponent<CapsuleCollider>());
            Destroy(GetComponent<MeshRenderer>());
            Destroy(GetComponent<MeshFilter>());

        }
        s = EState.dead;
    }

    #endregion

    #region Core Functions
    public virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag(glob.playerTag);
        cc = player.gameObject.GetComponent<CombatController>();
        GetComponent<AudioSource>().volume = 0;
        StartCoroutine(LoudIDK());
        StartCoroutine(ActionLoop());
    }

    public virtual void Update()
    {
        if (grounded())
        {
            // airMultiplier = 1f;
        }

        else
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y, rb.linearVelocity.z);
        }
    }

    IEnumerator LoudIDK()
    {
        yield return new WaitForSeconds(0.2f);
        GetComponent<AudioSource>().volume = 1;
    }
    #endregion
}
