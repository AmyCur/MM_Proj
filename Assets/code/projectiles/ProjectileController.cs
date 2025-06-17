using System;
using System.Collections;
using Globals;
using MasterCombatController;
using MathsAndSome;
using Unity.VisualScripting;
using UnityEngine;


// projectile move type
public enum PJMT{
    forward,
    homing
}


// projectiles can stack ontop of each other, thats a problem that can be fixed later
// If the projectile is directly above the player it moves incredibly slowly

// Im not sure if i want projectiles to home or not, but i think i want both so ill make both
[RequireComponent(typeof(Rigidbody))]
public abstract class ProjectileController : MonoBehaviour
{

    [Header("Parameters")]
    public float speed = 10f;
    public float maxHomingRange = 10f;
    public PJMT mt = PJMT.forward;
    Vector3 direction;
    public bool canBeAttacked = true;
    public int damage = 10;

    [HideInInspector] public Rigidbody rb;
    Vector3 playerDistance;
    [HideInInspector] public GameObject player;
    [HideInInspector] public Collider parent;

    public IEnumerator MoveForward(Vector3 r /* Rotation */){
        rb.linearVelocity = r * speed;
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(MoveForward(r));
    }

    public IEnumerator HomeToPlayer(){
        playerDistance = mas.player.PlayerDistance(player, gameObject);
        float distance = mas.vector.AddVectorComponents(playerDistance);
    
        if(distance > maxHomingRange){
            // Idk what the numbers in rotate towards are but i do know what a radian is
            // transform.rotation = mas.VectorsToQuaternion(transform.position, player.transform.position);
            // transform.rotation = mas.v3q(Vector3.RotateTowards(transform.position, player.transform.position, 2*Mathf.PI, 1f));
            direction = -(transform.position - player.transform.position).normalized;
        }

        rb.linearVelocity = direction * speed;
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(HomeToPlayer());
        
    }


    void Awake(){
        rb = GetComponent<Rigidbody>();
        player = mas.player.GetPlayer().gameObject;

        direction = Vector3.zero;

        if(mt == PJMT.forward){
            StartCoroutine(MoveForward(-(transform.position - player.transform.position).normalized));
        }
        else if(mt == PJMT.homing){
            StartCoroutine(HomeToPlayer());
        }

        if(this is ArrowController a){
            StartCoroutine(a.GetDeltaDistance());
        }

        // RespawnManager.Ins.entities.Add(gameObject);

        DestroyObj(15f);
    }

    public abstract IEnumerator DestroyObj(float delay);    

    void OnTriggerEnter(Collider other){
        
        if(other.tag == glob.playerTag){
            
            CombatController cc = other.GetComponent<CombatController>();
 
            if(cc!=null){
                MCC.TakeDamage(mas.player.GetCombatController(), damage, Vector3.zero, null, new RayData(AttackEnums.Attacker.projectile, 0, Vector3.zero, Vector3.zero));
            }

            StartCoroutine(DestroyObj(0f));
        }

        // Check to see if the other is a projectile
        if(other.GetComponent<ProjectileController>() == null && other.GetComponent<BaseEnemy>() == null){
            if(other != parent){
                StartCoroutine(DestroyObj(0f));
            }
        }
        
        
    }

    
}
