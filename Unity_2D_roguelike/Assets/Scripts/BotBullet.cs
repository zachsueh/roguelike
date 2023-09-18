using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotBullet : MonoBehaviour
{
    public float speed = 7.5f;
    public Rigidbody2D theRB;
    public GameObject impactEffect;
    private Vector3 direction;

    public int damageToGive = 50;

    // Start is called before the first frame update
    void Start()
    {
        direction = EnemyController.instance.transform.position - transform.position;
        direction.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
        //theRB.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);


        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyController>().DamageEnemy(damageToGive);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
