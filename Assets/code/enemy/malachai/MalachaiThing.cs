/*using UnityEngine;
using BossState;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class MalachaiThing : MonoBehaviour
{
    #region Variables
    [SerializeField] BS s;

    [Header("Statistics")]
    [Header("Health")]
    public int health;
    public int sbHealth;

    [Header("Damages")]
    public int fireballDamage;
    public int firewallDamage;

    [Header("Limits")]
    public float maxFlightTime;
    [Header("Menaces")]
    public int maxMenaces;
    public int maxMenacesPerSummon;
    #endregion



    readonly BS[] states = new BS[5] { BS.fireball, BS.firewall, BS.sb, BS.flying, BS.summoning };
    int currentState = 0;
    [SerializeField] GameObject fireball;
    [SerializeField] List<GameObject> fireballs = new();
    bool canStartAttack = true;


    public IEnumerator CreateFireball()
    {
        canStartAttack = false;
        GameObject fb = GameObject.Instantiate(fireball, transform.position, Quaternion.identity);
        fireballs.Add(fb);
        yield return new WaitForSeconds(3f);
        canStartAttack = true;
    }

    public IEnumerator CreateFirewall()
    {
        yield return new WaitForSeconds(3f);
    }

    public IEnumerator CreateSB()
    {
        yield return new WaitForSeconds(3f);
    }


    public IEnumerator Fly()
    {
        yield return new WaitForSeconds(3f);
    }

    public IEnumerator SummonMenaces()
    {
        yield return new WaitForSeconds(3f);
    }

    void DecideAttack()
    {
        BS bs = RandomAttack();
        switch (bs)
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

    BS RandomAttack()
    {
        List<BS> possibleStates = states.ToList();
        if (fireballs.Count > 5)
        {
            possibleStates.RemoveAt(0);
        }

        possibleStates.RemoveAt(currentState);
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

    IEnumerator AttackLoop()
    {
        if (canStartAttack) { DecideAttack(); }
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(AttackLoop());
    }

    void Start()
    {
        StartCoroutine(AttackLoop());
    }

}*/
