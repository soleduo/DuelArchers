using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour {

    public Rigidbody2D rigidBody;
    public Character chara;

    public float damage;
    public bool isHit;


    public Vector2 force;

    private void Awake()
    {
        if (rigidBody == null)
            rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.gravityScale = 0;

        
    }

    private void Start()
    {
        force = CalculateForce();
    }

    public void StartThrow(float power)
    {
        isHit = false;

        transform.position = chara.transform.position + new Vector3(0, 1f);
        transform.rotation = chara.transform.rotation;
        rigidBody.velocity = (new Vector2(3, 3 ) + (GameManager.physData.throwVelocity * (power)));
        rigidBody.velocity = new Vector2(rigidBody.velocity.x * Mathf.Sign(transform.right.x), rigidBody.velocity.y);
        //print(Time.deltaTime);

        chara.slider.value = 0;

    }

    public void Update()
    {
        rigidBody.AddForce(force);        
    }

    public Vector2 CalculateForce()
    {
        Vector2 force = new Vector2(GameManager.windForce * GameManager.windDir * Time.deltaTime, -1 * GameManager.gravity);

        return force;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public IEnumerator OnCollisionEnter2D(Collision2D collision)
    {
        yield return new WaitForFixedUpdate();

        isHit = true;

        yield return new WaitForSeconds(0.4f);

        Hide();

        GameManager.NextTurn();
    }



}
