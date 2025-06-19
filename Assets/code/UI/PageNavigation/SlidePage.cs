using UnityEngine;
using MathsAndSome;
using System.Collections;
using Magical;
using System;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using Slide;

/*
public class BezierCurve{

   

    public IEnumerator CalcualteCurve(){
        
    }

    public double bez(double t, double[] coefficients){
        double[] beta = coefficients;
        int n = beta.length;
        for (int i = 1; i < n; i++) {
            for (int j = 0; j < (n - i); j++) {
                beta[j] = beta[j] * (1 - t) + beta[j + 1] * t;
            }
        }
        return beta[0];
    }
}
*/

namespace Slide
{
    public enum Direction
    {
        up,
        down,
        left,
        right
    }
}


public class SlidePage : MonoBehaviour
{
    public Vector2 targetPosition;
    public Vector2 distance;
    [SerializeField] float moveTime;
    [SerializeField] bool fastMenu;
    // [SerializeField] double increment = 0.0002;
    [SerializeField] float x;
    [SerializeField] float y;
    Vector2Int direction;
    Vector3 preMPos;
    Vector2Int coords = Vector2Int.zero;
    bool br;
    // Spline spline;
    // SplineAnimate curve;
    bool moving;

    void Start()
    {
        //1134
        //1697 BETWIXT
        //2.4

        coords = Vector2Int.zero;
        float w = (float)Screen.width;
        float h = (float)Screen.height;

        if (Math.Round(w / h, 2) != 1.77)
        {
            Debug.Log("Resolution isnt 16:9");

            h *= (w / h) / 1.77f;
        }

        distance = new Vector2(
            distance.x *= w / x,
            distance.y *= h / y
        );

        Debug.Log(w);
        Debug.Log(h);
        Debug.Log(w / h);

    }

    /*
    public void Slide(float t, Vector3 targetPos){
        if(moving){
                br=true;
                
                if(direction==0){
                    if(fastMenu){
                        transform.position = targetPosition;
                    }

                    StartCoroutine(GoToTargetPosition(t, targetPos));
                }
                else{
                    StartCoroutine(GoToTargetPosition(0, preMPos));
                }
        }
        else{
            targetPosition = new Vector2(
                transform.position.x + distance.x, 
                transform.position.y + distance.y
            );
            
            if(fastMenu){
                transform.position = targetPosition;
            }

            else{
                preMPos = transform.position;
                StartCoroutine(GoToTargetPosition(t, targetPos));
            }
        }
    }
*/

    public void MoveDirection(float m, Direction dir)
    {
        Vector2Int dv = new Vector2Int();

        switch (dir)
        {
            case Direction.up:
                dv = Vector2Int.down;
                coords = new Vector2Int(coords.x, coords.y - 1);
                break;
            case Direction.down:
                dv = Vector2Int.up;
                coords = new Vector2Int(coords.x, coords.y + 1);
                break;
            case Direction.left:
                dv = Vector2Int.right;
                coords = new Vector2Int(coords.x - 1, coords.y);
                break;
            case Direction.right:
                dv = Vector2Int.left;
                coords = new Vector2Int(coords.x + 1, coords.y);
                break;
            default:
                Debug.Log("Unadded movement direction!");
                break;
        }

        if (moving)
        {
            br = true;

            targetPosition = new Vector2(
                targetPosition.x + distance.x * m * dv.x,
                targetPosition.y + distance.y * m * dv.y
            );

            StartCoroutine(GoToTargetPosition(0, targetPosition));


        }

        else
        {
            targetPosition = new Vector2(
                transform.position.x + distance.x * m * dv.x,
                transform.position.y + distance.y * m * dv.y
            );

            if (fastMenu)
            {
                transform.position = targetPosition;
            }
            else
            {
                preMPos = transform.position;
                StartCoroutine(GoToTargetPosition(0, targetPosition));
            }
            // Debug.Log(new BezierCurve(1f, 2f));
        }
    }
    /*
        public void MoveLeft(float m){
            direction=new Vector2Int(0, direction.y);
            Debug.Log("Left");

            if(moving){
                br=true;

                if(direction.x==0){
                    targetPosition = new Vector2(
                        targetPosition.x + distance.x*m, 
                        targetPosition.y 
                    );

                    StartCoroutine(GoToTargetPosition(0, targetPosition));
                }
                else{
                    StartCoroutine(GoToTargetPosition(0, preMPos));
                }
            }

            else{
                targetPosition = new Vector2(
                    transform.position.x + distance.x*m, 
                    transform.position.y
                );

                if(fastMenu){
                    transform.position = targetPosition;
                }
                else{
                    preMPos = transform.position;
                    StartCoroutine(GoToTargetPosition(0, targetPosition));
                }
                // Debug.Log(new BezierCurve(1f, 2f));
            }
        }

        public void MoveUp(float m){
            direction=new Vector2Int(direction.x, 0);
            Debug.Log("Left");

            if(moving){
                br=true;

                if(direction.y==0){
                    targetPosition = new Vector2(
                        targetPosition.x + distance.x*m, 
                        targetPosition.y 
                    );

                    StartCoroutine(GoToTargetPosition(0, targetPosition));
                }
                else{
                    StartCoroutine(GoToTargetPosition(0, preMPos));
                }
            }

            else{
                targetPosition = new Vector2(
                    transform.position.x + distance.x*m, 
                    transform.position.y
                );

                if(fastMenu){
                    transform.position = targetPosition;
                }
                else{
                    preMPos = transform.position;
                    StartCoroutine(GoToTargetPosition(0, targetPosition));
                }
                // Debug.Log(new BezierCurve(1f, 2f));
            }
        }

        public void MoveRight(float m){
            direction=1;
            if(moving){
                br=true;
                if(direction==1){
                    targetPosition = new Vector2(
                        targetPosition.x - distance.x*m, 
                        transform.position.y

                    );

                    StartCoroutine(GoToTargetPosition(0, targetPosition));
                }
                else{
                    StartCoroutine(GoToTargetPosition(0, preMPos));
                }
            }

            else{
                Debug.Log("Right");
                targetPosition = new Vector2(
                    transform.position.x - distance.x*m, 
                    transform.position.y
                );
                if(fastMenu){
                    transform.position = targetPosition;
                }
                else{
                    preMPos = transform.position;
                    StartCoroutine(GoToTargetPosition(0, targetPosition));
                }
                // Debug.Log(new BezierCurve(1f, 2f));
            }
        }
    */
    void Update()
    {

        // if(magic.key.down(keys.left)){
        //     MoveDirection(1, Direction.left);
        // }
        //
        // if(magic.key.down(keys.right)){
        //     MoveDirection(1, Direction.right);
        // }
        //
        // if(magic.key.down(keys.up)){
        //     MoveDirection(1, Direction.up);
        // }
        //
        // if(magic.key.down(keys.down)){
        //     MoveDirection(1, Direction.down);
        // }

        if (Input.GetKeyDown(KeyCode.R))
        {
            float w = (float)Screen.width;
            float h = (float)Screen.height;

            if (Math.Round(w / h, 2) != 1.77)
            {
                Debug.Log("Resolution isnt 16:9");

                h *= (w / h) / 1.77f;
            }

            distance = new Vector2(
                distance.x *= w / x,
                distance.y *= h / y
            );

            Debug.Log(w);
            Debug.Log(h);
            Debug.Log(w / h);
        }


    }




    public IEnumerator GoToTargetPosition(float t, Vector3 targetPos)
    {
        moving = true;
        transform.position = mas.vector.LerpVectors(transform.position, targetPos, t);
        t += 0.004f;
        yield return new WaitForSeconds(0.01f);

        if (br)
        {
            br = false;
        }
        else
        {
            if (t < 1)
            {
                Debug.Log(t);
                StartCoroutine(GoToTargetPosition(t, targetPos));
            }
            else
            {
                // transform.position = new Vector3(
                //     (int)(Math.Round(transform.position.x / 800.0) * 800),
                //     transform.position.y,0);

                Debug.Log("fin");
                preMPos = transform.position;
                moving = false;
            }
        }



    }
}
