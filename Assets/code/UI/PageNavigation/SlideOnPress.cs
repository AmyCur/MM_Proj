using System;
using UnityEngine;
using Slide;

public class SlideOnPress : MonoBehaviour
{
    SlidePage p;
    [SerializeField] float t;
    [SerializeField] Direction md;
    [SerializeField] int times;


    void Start()
    {
        p = GameObject.FindGameObjectWithTag("Holder").GetComponent<SlidePage>();   
    }

    public void Slide(){
        if(times!=0){
            p.MoveDirection(times,md);
        }  
    }
        
    
}
