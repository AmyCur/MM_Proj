using System.Collections;
using Globals;
using MathsAndSome;
using UnityEngine;

public class MarqueeController : MonoBehaviour
{

    enum direction{
        left,
        right,
        up,
        down
    }

    [SerializeField] bool pingPong; 


    RectTransform rt=new RectTransform();
    Vector2 size = Vector2.zero;
    RectTransform ts;
    GameObject canvas=null;
    Vector2 canvasStartPos;


    Vector2 canvasOffset => new Vector2(
        canvas.transform.position.x-canvasStartPos.x,
        canvas.transform.position.y-canvasStartPos.y
    );

    Vector2 startPos => new Vector2(
        (
            (-size.x / 2)
            + (rt.gameObject.transform.position.x)
            + (GetComponent<RectTransform>().rect.width / 2)
            // + (canvasOffset.x)
        ), 

        (
           (rt.gameObject.transform.position.y)
            // + (canvasOffset.y)
        )
    );

    Vector2 endPos => new Vector2(
        (size.x / 2) + rt.gameObject.transform.position.x - (GetComponent<RectTransform>().rect.width / 2)
        // + canvasOffset.x
        ,
        rt.gameObject.transform.position.y
        // + canvasOffset.y
    );
    
    bool atEnd=>transform.position.x != endPos.x && transform.position.y != endPos.y;


    [SerializeField] direction d; 

    void Ping(){
        if(pingPong){
            if(d == direction.left){
                d=direction.right;
            }

            else if(d == direction.right){
                d=direction.left;
            }

            Debug.Log(d);
        }

        StartCoroutine(Move(0));
    }

    IEnumerator Move(float t){

        if(t==0){
            
        }
        

        yield return new WaitForSeconds(0.01f);
        
        if(d==direction.right){
            transform.position = mas.vector.LerpVectors(startPos, endPos, t);
            // If not at end, keep moving
            if((transform.position.x == endPos.x && transform.position.y == endPos.y) || t>=1){
                Debug.Log($"FAIL :: trans: {transform.position} || target: {endPos} || t: {t}" );
                Debug.Log("Restart");
                Ping();
                
                
            }
            // Else restart
            else{
                Debug.Log($"SUCCESS :: trans: {transform.position} || target: {endPos} || t: {t}");
                t+=0.01f;
                StartCoroutine(Move(t));
            }
        }

        else if(d==direction.left){
            transform.position = mas.vector.LerpVectors(endPos, startPos, t);
            // If not at end, keep moving
            if((transform.position.x == startPos.x && transform.position.y == startPos.y) || t>=1){
                Debug.Log($"FAIL :: trans: {transform.position} || target: {endPos} || t: {t}" );
                Debug.Log("Restart");
                Ping();                
            }
            // Else restart
            else{
                Debug.Log($"SUCCESS :: trans: {transform.position} || target: {endPos} || t: {t}");
                t+=0.01f;
                StartCoroutine(Move(t));
            }
        }
        
    }


    void Start()
    {
        rt = transform.parent.gameObject.GetComponent<RectTransform>();

        canvas=GameObject.FindGameObjectWithTag(glob.holderTag);
        canvasStartPos = canvas.transform.position;
        
        

        size = new Vector2(rt.rect.width,rt.rect.height);

        



        if(rt!=null){
            StartCoroutine(Move(0));
        }
        else{
            Debug.LogError($"Orphan Marquee: {gameObject.name}");
        }
    }
}
