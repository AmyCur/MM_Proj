using System.Collections;
using MathsAndSome;
using UnityEngine;

public class FireballController : ProjectileController
{
    public override IEnumerator DestroyObj(float delay)
    {
        yield return new WaitForSeconds(delay);
        //* Insert any animations or particles or player collsion etc here

        // Save on processing power by just destroying the object if its far enough away
        if (mas.vector.AddVectorComponents(mas.player.PlayerDistance(player, gameObject)) > 500f)
        {
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        if (parent != null)
        {
            Menace m = parent.GetComponent<Menace>();

            if (m != null)
            {
                m.fireballs.Remove(gameObject);
            }
        }
    }
}
