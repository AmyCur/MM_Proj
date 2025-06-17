using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BossUtils;
using System.Linq;
using MasterCombatController;
using VectorUtils;
using EnemyState;

namespace BossUtils
{
    public enum BS
    {
        fireball,
        firewall,
        sb, // Snow Barrier
        flying,
        summoning,
    }
    public static class Barrier
    {
        [System.Serializable]
        public class SnowBarrier
        {
            public float maxTime;
            public float blockage;

            public SnowBarrier(float maxTime = 1_000_000f, float blockage = 70f)
            {
                this.maxTime = maxTime;
                this.blockage = blockage;
            }
        }

        public static float CalculateBarrierStrength(SnowBarrier[] barriers)
        {
            float percentage = 0f;

            for (int i = 0; i < barriers.Length; i++)
            {
                percentage += barriers[i].blockage / i + 1;
            }

            return Mathf.Clamp(percentage, 0f, 100f);
        }
    }
}

public class MalachaiController : BaseEnemy
{
    [Space(20)]
    [Header("Malachai")]
    [Header("State")]
    [SerializeField] BS bs;

    [Header("Damages")]
    public int fireballDamage;
    public int firewallDamage;

    [Header("Limits")]
    public float maxFlightTime;
    [Header("Menaces")]
    public int maxMenaces;
    public int maxMenacesPerSummon;
    [SerializeField] bool canSpawnMenaces = true;

    readonly BS[] states = new BS[5] { BS.fireball, BS.firewall, BS.sb, BS.flying, BS.summoning };
    int currentState = 0;

    [Header("Objects")]
    [SerializeField] GameObject fireball;
    [SerializeField] GameObject firewall;
    [SerializeField] GameObject menace;
    List<GameObject> fireballs = new();
    List<GameObject> firewalls = new();
    List<GameObject> menaces = new();

    [Header("Attack Parameters")]
    [Header("Snow Barrier")]
    bool canSpawnBarrier = true;
    public List<Barrier.SnowBarrier> barriers = new();

    bool isBarrierActive;

    const float targetHeight = 33;

    public IEnumerator CreateFireball()
    {
        int fbc = Random.Range(3, 5);
        for (int j = 0; j < 3; j++)
        {
            for (int i = 0; i < fbc; i++)
            {
                GameObject fb = GameObject.Instantiate(fireball, transform.position, Quaternion.identity);
                fireballs.Add(fb);
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(3f);
        }
    }

    public IEnumerator CreateFirewall()
    {
        Vector3 firewallPosition = VU.GetRandomVectorInRadius(transform.position, 10f);
        if (MCC.Ray.ShootRay(new(null, 1_000_000, firewallPosition, Vector3.down), null) is RaycastHit r)
        {
            firewallPosition = r.transform.position + new Vector3
            (
                0,
                firewall.transform.localScale.y,
                0
            );

        }

        firewalls.Add(GameObject.Instantiate(firewall, firewallPosition, Quaternion.identity));
        yield return new WaitForSeconds(3f);
    }

    public IEnumerator CreateSB()
    {
        isBarrierActive = true;
        canSpawnBarrier = false;
        barriers.Add(new());

        yield return new WaitForSeconds(20f);
        canSpawnBarrier = true;
    }

    // FIX: Make flying go in the correct dirrection
    public IEnumerator Fly()
    {
        Vector3 randomVector = VU.GetRandomVectorInRadius(Vector3.zero, 1f);
        rb.linearVelocity = randomVector * 10f;

        yield return new WaitForSeconds(3f);
    }

    // TODO: Tweak menace spawning rates
    public IEnumerator SummonMenaces()
    {
        canSpawnMenaces = false;
        Vector3 menacePosition = VU.GetRandomVectorInRadius(transform.position, 10f);
        menaces.Add(GameObject.Instantiate(menace, menacePosition, Quaternion.identity));
        yield return new WaitForSeconds(8f);
        canSpawnMenaces = true;
    }

    void CheckFireballs()
    {
        int u = fireballs.Count;
        for (int i = 0; i < u; i++)
        {
            if (fireballs[i] == null)
            {
                fireballs.RemoveAt(i);
            }
        }
        u = firewalls.Count;
        for (int i = 0; i < u; i++)
        {
            if (firewalls[i] == null)
            {
                firewalls.RemoveAt(i);
            }
        }
        u = menaces.Count;
        for (int i = 0; i < u; i++)
        {
            if (menaces[i] == null)
            {
                menaces.RemoveAt(i);
            }
        }
    }

    void DecideAttack()
    {
        BS bst = RandomAttack();
        switch (bst)
        {
            case BS.fireball:
                StartCoroutine(CreateFireball());
                break;
            case BS.firewall:
                StartCoroutine(CreateFirewall());
                break;
            case BS.sb:
                StartCoroutine(CreateSB());
                break;
            case BS.flying:
                StartCoroutine(Fly());
                break;
            case BS.summoning:
                StartCoroutine(SummonMenaces());
                break;

        }
    }
    int x = 0;
    BS RandomAttack()
    {
        x++;
        if (x % 20 == 0)
        {
            x = 0;
            CheckFireballs();
        }
        List<BS> possibleStates = states.ToList();

        if (fireballs.Count >= 5)
        {
            possibleStates.Remove(BS.fireball);
        }

        if (firewalls.Count >= 1)
        {
            possibleStates.Remove(BS.firewall);
        }

        if (isBarrierActive || !canSpawnBarrier)
        {
            possibleStates.Remove(BS.sb);
        }

        if (menaces.Count() >= 5 || !canSpawnMenaces)
        {
            possibleStates.Remove(BS.summoning);
        }

        currentState = Random.Range(0, possibleStates.Count);

        for (int i = 0; i < states.Length; i++)
        {
            if (states[i] == possibleStates[currentState])
            {
                currentState = i;
                break;
            }
        }

        return states[currentState];
    }
    public override IEnumerator Attack()
    {
        attacking = true;
        // Random const for testing
        DecideAttack();
        yield return new WaitForSeconds(1.5f);
        attacking = false;
    }

    public override IEnumerator Hunt()
    {
        hunting = true;
        if (!attacking)
        {
            StartCoroutine(Attack());
        }
        yield return new WaitForSeconds(1f);
        hunting = false;
    }

    /*
        public IEnumerator GoToTargetHeight(Vector3 startPos, float t)
        {
            if (!br)
            {
                transform.position = VU.LerpVectors(startPos, new(startPos.x, targetHeight, startPos.z), t);
                if (t < 1)
                {
                    yield return new WaitForSeconds(0.01f);
                    t += 0.1f;
                    StartCoroutine(GoToTargetHeight(startPos, t));
                }
                else
                {
                    yield return null;
                    StartCoroutine(GoToTargetHeight(transform.position, 0));
                }
            }
        }
    */
    public override void Start()
    {
        base.Start();
        attacking = false;
        // StartCoroutine(GoToTargetHeight(transform.position, 0));
    }
}
