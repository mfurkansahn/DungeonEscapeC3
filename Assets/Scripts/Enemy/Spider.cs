using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy, IDamageable
{
    public GameObject acidEffectPrefab;
    public int Health { get; set; }

    //Use for initialization
    public override void Init()
    {
        base.Init();

        //assign Health property to our enemy health 
        Health = base.health;
    }

    public override void Update()
    {

    }
    

    public void Damage()
    {
        if (isDead == true)
            return;

        Debug.Log("Spider::Damage()");
        Health--;

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

    public override void Movement()
    {
        //sit still
    }

    public void Attack()
    {
        //instantiate the acid effect
        Instantiate(acidEffectPrefab, transform.position, Quaternion.identity);
    }
}
