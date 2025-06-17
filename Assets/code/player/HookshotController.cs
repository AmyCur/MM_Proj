using System;
using System.Collections;
using UnityEngine;
using MathsAndSome;
using Globals;
using MasterCombatController;
using Magical;
using Knifes;

namespace Knifes
{
    [Serializable]
    public class Knife
    {
        [Range(50, 1000)] public float range;
        public float cdTime;
        [Range(3, 20)] public float truthNukeRange;
        public int knifeDamage;
        public float? upForce;

        public Knife(float range, float cdTime, float truthNukeRange, int knifeDamage, float? upForce)
        {
            this.range = range;
            this.cdTime = cdTime;
            this.truthNukeRange = truthNukeRange;
            this.knifeDamage = knifeDamage;
            this.upForce = upForce;
        }
    }
}

public class HookshotController : MonoBehaviour
{

    [Header("Hookshot")]

    public Knife knife = new(
        range: 100f,
        cdTime: 0.2f,
        truthNukeRange: 3f,
        knifeDamage: 1,
        upForce: 10f
    );

    

    [HideInInspector] public bool canHook = true;
    bool shouldHook => canHook && magic.key.down(keys.hook);
    bool shouldBreak;


    PlayerController pc;
    BaseEnemy enemy;



    void Start()
    {
        pc = GetComponent<PlayerController>();
    }

    IEnumerator HookCD()
    {
        StartCoroutine(GameObject.FindGameObjectWithTag("Canvas").GetComponent<EnableOnStart>().LerpHookshotColour(knife.cdTime / 20));
        canHook = false;
        yield return new WaitForSeconds(knife.cdTime);
        canHook = true;
    }

    IEnumerator PullEnemy(RaycastHit hit, BaseEnemy ec, float a)
    {
        enemy = ec;
        float f = 0.01f;
        yield return new WaitForSeconds(f);
        ec.transform.position = mas.vector.LerpVectors(hit.transform.position, transform.position, a);

        if (a < 1 && !shouldBreak)
        {
            a += f;
            StartCoroutine(PullEnemy(hit, ec, a));
        }
        else
        {
            enemy = null;
        }

        shouldBreak = false;

    }
    void Hook()
    {
        if (Physics.Raycast(transform.position, pc.playerCamera.transform.forward, out RaycastHit hit, knife.range, glob.enemyMask))
        {
            BaseEnemy ec = hit.collider.GetComponent<BaseEnemy>();

            if (ec != null)
            {
                StartCoroutine(PullEnemy(hit, ec, 0f));
            }
        }

        StartCoroutine(HookCD());
    }

    void StabEnemy()
    {
        Vector3 direction = (enemy.transform.position - transform.position).normalized;
        if (Physics.Raycast(
            transform.position,
            enemy.transform.position - transform.position,
            out RaycastHit hit,
            10_000f,
            glob.enemyMask
        ))
        {
            MCC.TakeDamage(enemy, knife.knifeDamage, direction, knife.upForce, new RayData(AttackEnums.Attacker.hookshot, 0, Vector3.zero, Vector3.zero));
            // enemy.TakeDamage(knifeDamage, direction);
        }

    }


    void Update()
    {
        if (shouldHook)
        {
            Hook();
        }
        if (enemy != null)
        {
            if (mas.isInRadiusToPoint(enemy.transform.position, transform.position, glob.playerTag, knife.truthNukeRange))
            {
                shouldBreak = true;
                StabEnemy();
            }
        }
    }

}
