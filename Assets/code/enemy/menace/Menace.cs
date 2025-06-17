using System.Collections;
using System.Collections.Generic;
using MathsAndSome;
using UnityEngine;
using EnemyState;


// The menace flies away :(

[RequireComponent(typeof(Rigidbody))]
public class Menace : BaseEnemy
{
    Vector2 playerOffset;

    bool moving;

    [SerializeField] float offset = 10f;
    [SerializeField] float[] speed = new float[2];

    /*[HideInInspector]*/
    public List<GameObject> fireballs;

    public GameObject fireball;

    IEnumerator MoveMenace()
    {
        moving = true;

        // This should move it a bit
        Vector3 playerDistance = mas.player.PlayerDistance(player, gameObject);

        if (playerDistance.x > 5f || playerDistance.z > 5f)
        {
            Vector3 direction = -(transform.position - player.transform.position).normalized;
            rb.linearVelocity = new Vector3(direction.x, 0, direction.z) * Random.Range(speed[0], speed[1]);
        }

        else if (playerDistance.x < 2f || playerDistance.z < 2f)
        {
            rb.linearVelocity = Vector3.zero;
        }

        else
        {
            rb.linearVelocity = new Vector3(Random.Range(-offset / 2, offset / 2), 0, Random.Range(-offset / 2, offset / 2));
        }

        yield return new WaitForSeconds(0.3f);

        moving = false;
    }

    void ShootFireball()
    {
        if (fireballs.Count < projLimit)
        {
            GameObject fb = Instantiate(fireball, transform.position, Quaternion.identity);
            fb.GetComponent<ProjectileController>().parent = GetComponent<CapsuleCollider>();
            fireballs.Add(fb);
        }
    }

    #region Override Functions
    public override IEnumerator Attack()
    {
        attacking = true;

        int randomNumber = Mathf.FloorToInt(Random.Range(0, 9.999f));

        if (randomNumber == 0 && !moving)
        {
            StartCoroutine(MoveMenace());
        }
        else
        {
            ShootFireball();
        }

        yield return new WaitForSeconds(attackTime);

        attacking = false;
    }

    public override IEnumerator Hunt()
    {
        // Debug.Log("Hunt");
        hunting = true;

        if (inAttackRange())
        {
            s = EState.attacking;
        }

        yield return new WaitForSeconds(0.1f);

        hunting = false;
    }
    #endregion
    #region Core Functions
    public override void Start()
    {
        base.Start();
        fireballs = new List<GameObject>();
    }


    #endregion
}
