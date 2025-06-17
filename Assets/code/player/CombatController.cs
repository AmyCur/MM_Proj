using System.Collections;
using MathsAndSome;
using UnityEngine;
using Globals;
using MasterCombatController;
using UnityEngine.UI;
using Magical;

public class CombatController : MonoBehaviour
{
    GameObject deathScreen;

    public int health;
    [HideInInspector] public KeyCode atkKey = KeyCode.Mouse0;
    public bool canAttack;
    bool shouldAttack => canAttack && magic.key.down(keys.attack);
    MeshRenderer bowMesh;
    [SerializeField] LayerMask hitLayers;

    [Header("Weapons")]

    [SerializeField] BowScriptable[] bows;
    public BowScriptable currentWeapon;
    [HideInInspector] public bool canSwitchWeapon = true;

    [SerializeField] Slider healthSlider;

    [HideInInspector] public Coroutine cr;
    [HideInInspector] public Coroutine er;
    [HideInInspector] public Coroutine slowTimeCR;

    [Header("Sounds")]

    [Header("Bow")]

    public AudioClip bowshoot;
    public AudioClip bowHit;
    public AudioClip bowKill;

    Vector3 sPos => transform.position;
    Vector3 tPos => Camera.main.transform.forward;

    #region Coroutines
    // Parses so that if you switch weapon, it will end the cd of other weapon and not this one (this is probably bad)
    IEnumerator Attack(BowScriptable w)
    {

        canAttack = false;

        RayData attackData = new RayData(AttackEnums.Attacker.player, w.range, transform.position, tPos);

        if (MCC.Ray.ShootRay(attackData, glob.enemyMask) is RaycastHit hit)
        {

            BaseEnemy be = hit.collider.GetComponent<BaseEnemy>();

            if (be != null)
            {
                MCC.TakeDamage(be, w.damage, Camera.main.transform.forward, w.upForce, attackData);
            }

        }
        else if (MCC.Ray.ShootRay(attackData, 0) is RaycastHit h)
        {
            if (w.e.explode)
            {
                MCC.Explosion.CreateExplosion(new(null, w.e.radius, h.transform.position));
            }
        }

        yield return new WaitForSeconds(w.atkCD);

        canAttack = true;

    }
    #endregion
    #region Functions
    void SwitchWeapons()
    {
        if (canSwitchWeapon)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && bows[0] != null)
            {
                currentWeapon = bows[0];
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) && bows[1] != null)
            {
                currentWeapon = bows[1];
            }
            if (Input.GetKeyDown(KeyCode.Alpha3) && bows[2] != null)
            {
                currentWeapon = bows[2];
            }
            if (Input.GetKeyDown(KeyCode.Alpha4) && bows[3] != null)
            {
                currentWeapon = bows[3];
            }

            if (gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<MeshFilter>().mesh != currentWeapon.mesh)
            {
                gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<MeshFilter>().mesh = currentWeapon.mesh;
            }

        }
    }

    public void SetHealth(float newValue)
    {
        StartCoroutine(LerpHealth((float)health / 100f, newValue / 100f, 0));
        health = (int)newValue;
    }

    public IEnumerator LerpHealth(float start, float end, float t)
    {
        healthSlider.value = Mathf.Lerp(start, end, t);
        yield return new WaitForSeconds(0.04f);

        if (t < 1f)
        {
            StartCoroutine(LerpHealth(start, end, t + 0.1f));
        }
    }

    IEnumerator DeathSlowDown(float t)
    {
        Time.timeScale = Mathf.Lerp(1, 0, t);
        yield return new WaitForSeconds(0.01f);
        t += 0.02f;
        Debug.Log(t);
        if (t < 1f)
        {
            slowTimeCR = StartCoroutine(DeathSlowDown(t));
        }
        else
        {
            Time.timeScale = 0.000001f;
        }
    }

    public void Die()
    {

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        deathScreen.SetActive(true);


        StartCoroutine(DeathSlowDown(0));



        //TODO: PlayDeathAnimation
        //TODO: DoAdminStuff
        //TODO: AllowForRespawn
    }



    #endregion

    #region Core Functions
    void Start()
    {
        deathScreen = GameObject.FindGameObjectWithTag(glob.deathScreenTag);
        deathScreen.SetActive(false);
        canAttack = true;
        currentWeapon = bows[0];
        // StartCoroutine(LerpHealth(health/100, health/100, 0));

    }
    void Update()
    {

        if (shouldAttack)
        {
            // Debug.Log("Should");
            StartCoroutine(Attack(currentWeapon));
        }

        if (magic.key.down(keys.killAllKey))
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(glob.enemyTag);

            foreach (GameObject gol in enemies)
            {
                MCC.TakeDamage(gol.GetComponent<BaseEnemy>(), 1_000_000_000, Vector3.zero, 0, new RayData(AttackEnums.Attacker.silent, 0, Vector3.zero, Vector3.zero));
            }
        }

        SwitchWeapons();
    }
    #endregion

}
