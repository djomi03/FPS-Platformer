using System;
using System.Collections;
using UnityEngine;

public class turret : MonoBehaviour
{
    public GameObject Player;
    public GameObject Bullet;
    public GameObject Card;
    public float timer = 4;
    public float bulletTime = 4;
    public float health = 3;
    public bool canBeHit = true;
    Vector3 offset = new Vector3(0, -1f, 0);
    // Update is called once per frame
    void Update()
    {
        Vector3 targetDirection = -Player.transform.position + this.transform.position;
        float singleStep = 5.0f * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        Debug.DrawRay(transform.position, newDirection, Color.red);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        bulletTime -= Time.deltaTime;

        if (bulletTime > 0) return;

        bulletTime = timer;


        GameObject instance = Instantiate(Bullet, this.transform.position, Quaternion.identity);
        Rigidbody bulletRB = instance.GetComponent<Rigidbody>();
        bulletRB.AddForce(-this.transform.forward * 500);
        Destroy(instance, 5f);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        CheckDead();
    }
    public void CheckDead() 
    {
        if (health <= 0)
        {
            canBeHit = false;
            Destroy(this.gameObject);
            if (Card)
            {
                Instantiate(Card, transform.position + offset, Quaternion.identity);
            }
            return;
        }
    }
}


//float angleStep = coneAngle / 4;

//for (int i = 0; i < 5; i++)
//{
//    // Calculate the angle for this ray
//    float angle = -coneAngle / 2 + angleStep * i + 180;

//    // Rotate the forward direction to get the direction for this ray
//    Vector3 direction = Quaternion.Euler(0, angle, 0) * transform.forward;

//    // Cast the ray
//    RaycastHit hit;
//    if (Physics.Raycast(transform.position, direction, out hit, rayDistance))
//    {
//        if (hit.transform.gameObject.name == "Player")
//        {
//            PlayerDetected();
//            Debug.Log("Player detected !");
//        }

//    }
//}