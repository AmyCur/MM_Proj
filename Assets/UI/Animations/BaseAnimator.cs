using System.Collections;
using UnityEngine;
using TMPro;

[System.Flags]
public enum animType{
    text_size_increase,
    text_colour_change
}

[System.Serializable]
public struct TextSize{
    public TMP_Text text; 
    public float waitTime;
    public float mult;
    public float textSize;
    public float targetSize;
}

public class BaseAnimator : MonoBehaviour
{
    
    public animType aType;
    public TextSize ts;

    public void Start()
    {
        ts.textSize = ts.text.fontSize;
        ts.targetSize = ts.text.fontSize * 1.2f;
    }

    public  void StartAnimation()
    {    
        StopAllCoroutines();
        if(aType == animType.text_size_increase){
            
            StartCoroutine(IncreaseSize((TextSize)ts));
            
        }
        
    
    }

    public void EndAnimation()
    {
        StopAllCoroutines();
        if(aType == animType.text_size_increase){
           
            StartCoroutine(ReduceSize((TextSize)ts));
            
        }
    }

    #region Text Size (ts)
    IEnumerator IncreaseSize(TextSize s){
        s.text.fontSize *= s.mult;
        yield return new WaitForSeconds(s.waitTime);
        
        if(s.text.fontSize >= s.targetSize){
            Debug.Log("Done");
        }
        else{
            StartCoroutine(IncreaseSize(s));
        }
    }

    IEnumerator ReduceSize(TextSize s){
        s.text.fontSize /= s.mult;
        yield return new WaitForSeconds(s.waitTime);
        
        if(s.text.fontSize <= s.textSize){
            Debug.Log("Done");
        }
        else{
            StartCoroutine(ReduceSize(s));
        }
    }
    #endregion

    

}
