using System.Collections;
using System.Collections.Generic;
using Globals;
using MathsAndSome;
using UnityEngine;
using EnemyState;
using MasterCombatController;

// No [RequireComponent(typeof(NavMeshAgent))] because i might need to add 
// and remove it for the ai to work properly

//! The ai is STUPID so ALL objects MUST be curved!!!!!!!!!

[RequireComponent(typeof(Rigidbody))]
public class SwordsMan : BaseEnemy
{

    public float moveSpeed = 12f;
    [SerializeField] float pushForce = 2f;
    public Vector3 Direction(Vector3 targetPos)
    {
        Vector3 td = (targetPos - transform.position).normalized;
        return new Vector3(td.x, 0, td.z);
    }

    [Header("Animation")]

    public Animator anim;

    void Push()
    {
        GameObject[] g = GameObject.FindGameObjectsWithTag(glob.swordsmanTag);
        List<SwordsMan> swordsMen = new List<SwordsMan>();

        foreach (GameObject obj in g)
        {
            if (obj != gameObject)
            {
                Vector3 distance = mas.vector.AbsVector(mas.vector.zeroY(transform.position - obj.transform.position));
                float dF = distance.x + distance.z;

                if (dF < 2)
                {
                    Vector3 lv = rb.linearVelocity;
                    rb.linearVelocity = new Vector3(
                        lv.x + distance.normalized.x * pushForce,
                        lv.y,
                        lv.z + distance.normalized.z * pushForce
                    );
                }
            }
        }
    }

    #region Combat Overrides
    public override IEnumerator Hunt()
    {
        Debug.Log("Hunt");
        hunting = true;

        Vector3 d = Direction(player.transform.position) * moveSpeed;

        if (grounded())
        {
            rb.linearVelocity = new Vector3(d.x, rb.linearVelocity.y, d.z);
        }

        if (inAttackRange())
        {
            s = EState.attacking;
        }

        yield return new WaitForSeconds(0.1f);

        hunting = false;
    }
    public override IEnumerator Attack()
    {

        // Debug.Log("Attack");
        attacking = true;

        // rb.linearVelocity = Vector3.zero;
        Vector3 pd = mas.player.PlayerDistance(player, gameObject);

        bool playerInRange = pd.x <= attackRange &&
        pd.z <= attackRange &&
        pd.y <= maxYRange;

        if (!playerInRange)
        {
            s = EState.hunting;
        }

        else
        {
            MCC.TakeDamage(mas.player.GetCombatController(), damage, Vector3.zero, null, new RayData(AttackEnums.Attacker.swordsman, 0, Vector3.zero, Vector3.zero));
        }

        yield return new WaitForSeconds(attackTime);

        attacking = false;
    }
    #endregion

    #region Core Functions
    public override void Update()
    {
        base.Update();
        anim.SetBool("attacking", attacking);

        Push();
    }

    public override void Start()
    {
        base.Start();
        anim = gameObject.transform.GetChild(0).GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }
    #endregion
}
