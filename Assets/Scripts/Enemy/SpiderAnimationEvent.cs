using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAnimationEvent : MonoBehaviour
{
    //handle to the spider
    private Spider _spider;

    private void Start()
    {
        //assign handle to the spider
        _spider = transform.parent.GetComponent<Spider>();
    }
   public void Fire()
    {
        //Tell Spider to fire
        Debug.Log("Spider should Fire!");
        //use handle to call Attack method on spider
        _spider.Attack();
    }

}
