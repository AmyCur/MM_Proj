using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundImageManager : MonoBehaviour
{
    // This should contain the images
    [SerializeField] Image[] images;
    int ci=0;
    
    public IEnumerator FadeImages(float t){
        int oi = ci == 0 ? 1:0;
        
        Color c = images[ci].color;
        
        images[ci].color = new Color(
            c.r,
            c.g,
            c.b,
            1-t
        );

        c = images[oi].color;
        
        images[oi].color = new Color(
            c.r,
            c.g,
            c.b,
            t
        );
        

        t+=0.1f;
        yield return new WaitForSeconds(0.1f);

        if(t<1){
            StartCoroutine(FadeImages(t));
        }

        else{
            ci=oi;
            Debug.Log(ci);
        }
    }


}
