using UnityEngine;
using TMPro;
using System.Collections;

public class IncreaseTextSize : BaseAnimator
{
    /*
    public TMP_Text text; 
    public float waitTime = 0.01f;
    public float mult = 1.05f;
    float textSize;
    float targetSize;

    public void Start()
    {
        textSize = text.fontSize;
        targetSize = text.fontSize * 1.2f;
    }

    public override void StartAnimation()
    {
        base.StartAnimation();
        
        
        StartCoroutine(IncreaseSize());
    
    }

    public override void EndAnimation()
    {
        base.EndAnimation();
        StartCoroutine(ReduceSize());
    }

    IEnumerator IncreaseSize(){
        text.fontSize *= mult;
        yield return new WaitForSeconds(waitTime);
        
        if(text.fontSize >= targetSize){
            text.fontSize = targetSize;
            Debug.Log("Done");
        }
        else{
            StartCoroutine(IncreaseSize());
        }
    }

    IEnumerator ReduceSize(){
        text.fontSize /= mult;
        yield return new WaitForSeconds(waitTime);
        
        if(text.fontSize <= textSize){
            text.fontSize = textSize;
            Debug.Log("Done");
        }
        else{
            StartCoroutine(ReduceSize());
        }
    }*/
}
