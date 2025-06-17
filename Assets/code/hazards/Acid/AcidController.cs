using UnityEngine;
using Globals;
using System.Collections;
using MasterCombatController;

public class AcidController : MonoBehaviour
{
    Coroutine cr;

    public int damage;
    public float time;

    IEnumerator TakeDamage(CombatController cc)
    {
        MCC.TakeDamage(cc, damage, Vector3.zero, 0, null);
        yield return new WaitForSeconds(time);
        if (cr != null)
        {
            StartCoroutine(TakeDamage(cc));
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == glob.playerTag && cr == null)
        {
            cr = StartCoroutine(TakeDamage(other.collider.GetComponent<CombatController>()));
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.collider.tag == glob.playerTag)
        {
            StopCoroutine(cr);
            cr = null;
        }
    }
}
