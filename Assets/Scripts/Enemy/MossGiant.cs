using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossGiant : Enemy, IDamageable
{
    public int Health { get; set; }

    //Use for initialize
    public override void Init()
    {
        base.Init();

        //assign Health property to our enemy health 
        Health = base.health;
    }

    public override void Movement()
    {
        base.Movement();
    }

    public void Damage()
    {
        if (isDead == true)
            return;

        Debug.Log("MossGiant::Damage()");

        //substract 1 from health
        Health--;
        anim.SetTrigger("Hit");
        isHit = true;
        anim.SetBool("InCombat", true);

        //if health is less than 1 
        //destroy the object
        if (Health < 1)
        {
            isDead = true;
            anim.SetTrigger("Death");
            //spawn a diamond 
            GameObject diamond = Instantiate(diamondPrefab, transform.position, Quaternion.identity) as GameObject;
            //change value of diamond to whatever my gem count is.
            diamond.GetComponent<Diamond>().gems = base.gems;
        }
    }
}
