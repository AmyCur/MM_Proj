using UnityEngine;
using System.Collections;
using VectorUtils;
using Magical;
using System.Collections.Generic;

public class NewMenu : MonoBehaviour
{
    Camera c;
    [SerializeField] List<IEnumerator> q = new();
    bool executingAction;

    void Start()
    {
        c = GetComponent<Camera>();
    }

    public void PrintLE(List<IEnumerator> a)
    {
        foreach (var i in a)
        {
            Debug.Log(i.ToString());
        }
    }


    public IEnumerator MoveToScreen(Vector3 startPos, Vector3 screen, float x)
    {
        executingAction = true;
        c.transform.position = VectorUtils.VU.LerpVectors(startPos, screen, x);

        if (x < 1)
        {
            x += 0.01f;
            yield return new WaitForSeconds(0.01f);
            StartCoroutine(MoveToScreen(startPos, screen, x));
        }
        else
        {
            executingAction = true;
            q.RemoveAt(0);
        }

        yield return null;
    }

    void Update()
    {
        if (magic.key.down(keys.right))
        {

            q.Add(MoveToScreen(
                  transform.position,
                  new(
                  transform.position.x + 20,
                  transform.position.y,
                  transform.position.z
                  ), 0f
                )
            );
        }

        if (q.Count > 0)
        {
            if (!executingAction)
            {
                StartCoroutine(q[0]);
            }
        }

        PrintLE(q);
    }
}

