using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    //handle to animator
    private Animator _anim;
    //reference to sword animation
    private Animator _swordAnimation;

    //Use this for initalization
    // Start is called before the first frame update
    void Start()
    {
        //assign handle to animator
        _anim = GetComponentInChildren<Animator>();
        _swordAnimation = transform.GetChild(1).GetComponent<Animator>();
    }

    public void Move(float move)
    {
        //anim set float Move , move
        _anim.SetFloat("Move", Mathf.Abs(move));
    }

    public void Jump(bool jumping)
    {
        _anim.SetBool("Jumping", jumping);
    }

    //Attack Method
    public void Attack()
    {
        _anim.SetTrigger("Attack");
        //play sword animation
        _swordAnimation.SetTrigger("SwordAnimation");
    }

    public void Death()
    {
        _anim.SetTrigger("Death");

    }
}
