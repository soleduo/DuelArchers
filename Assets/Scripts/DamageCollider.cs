using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour {

    public Character owner;

    public float damageMod;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Projectile projectile = null;

        

        if (collision.gameObject.tag == "Projectile")
        {
            projectile = collision.gameObject.GetComponent<Projectile>();
        }

        

        if (projectile != null && projectile.chara != owner && projectile.isHit == false)
        {
            print("TryApplyDamage");
            owner.ApplyDamage(projectile.damage * damageMod);
        }

        
    }

}
