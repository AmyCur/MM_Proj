using UnityEngine;
using System.Collections
;
public class FadeInOutNotification : NotificationManager
{
    public override IEnumerator IntroEffect(float t = 0)
    {



        yield return new WaitForSeconds(introTime);

        if (t + 0.1 < 1)
        {
            StartCoroutine(IntroEffect(t + 0.1f));
        }
    }

    public override IEnumerator OutroEffect(float t = 0)
    {

        yield return new WaitForSeconds(introTime);

        if (t + 0.1 < 1)
        {
            StartCoroutine(IntroEffect(t + 0.1f));
        }
    }
}
