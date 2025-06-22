using UnityEngine;
using System.Collections;

public class DisabledEnemy : BaseEnemy
{
    public override IEnumerator Hunt()
    {
        yield return null;
    }

    public override IEnumerator Attack()
    {
        yield return null;
    }
}
