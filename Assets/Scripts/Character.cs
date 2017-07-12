using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour {

    public float health = 100f;
    public Slider healthSlider;
    public float power = 0.5f;

    public int index;

    [Space]
    public bool isPowerUp;
    public PowerUp p_up;
    public float damage;

    [Space]

    public Projectile projectile;
    public Button btn;
    public Slider slider;

    [Space]
    public Collider2D headCollider;
    public Collider2D bodyCollider;

    bool isClicked = false;
    

    public void ThrowInit()
    {
        if (slider.gameObject.activeInHierarchy == false)
            slider.gameObject.SetActive(true);

        if (Input.GetMouseButton(0) && slider.value <= 1)
        {

            slider.value += Time.deltaTime;

            print("Throw Init");


        }
        else
        {
            
            ApplyPower();
            isClicked = false;
        }

    }

    private void Update()
    {
        if(isClicked)
        {
            ThrowInit();
        }
    }

    public void ApplyPower()
    {
        power = slider.value;

        slider.gameObject.SetActive(false);

        if (projectile.gameObject.activeInHierarchy == false)
            projectile.gameObject.SetActive(true);

        SetProjectileDamage(damage);

        projectile.StartThrow(power);


    }

    public void SetProjectileDamage(float damage)
    {
        projectile.damage = damage;
    }

    public void ApplyDamage(float damage)
    {
        health -= damage;
        healthSlider.value = health / 100;

        print("ApplyDamage " + damage);
    }

    public void OnHeadHit()
    {

    }

    public void OnBodyHit()
    {

    }

    private void OnMouseDown()
    {
        if (GameManager.turn == index && isClicked != true)
            isClicked = true;
    }

    public void ApplyPowerUp(PowerUp type)
    {
        
    }
}

public enum PowerUp
{

}