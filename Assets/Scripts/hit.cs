using UnityEngine;

public class hit : MonoBehaviour
{
    public int Gundamage;

    public GameObject Player;

    private void Start()
    {
        Destroy(this.gameObject, 10);
        GunSystem Gun = Player.transform.GetComponent<GunSystem>();
        //if (Gun.gunType == "Yellow")
        //{
        //    Gundamage = 1;
        //}
        //if (Gun.gunType == "Red")
        //{
        //    Gundamage = 1;
        //}
        //if (Gun.gunType == "Purple")
        //{
        //    Gundamage = 5;
        //}
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "YellowEnemy" || other.gameObject.tag == "RedEnemy" || other.gameObject.tag == "PurpleEnemy")
        {
            turret Turret = other.transform.GetComponent<turret>();
            
            if (Turret != null && Turret.canBeHit)
            {
                Turret.TakeDamage(Gundamage);
            }
            Destroy(this.gameObject);
        }
        if (other.gameObject.tag == "EnemyBullet")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Enemy")
    //    {
    //        turret Turret = collision.transform.GetComponent<turret>();

    //        if (Turret != null)
    //        {
    //            Turret.TakeDamage(Gundamage);
    //        }
    //        Destroy(this.gameObject);
    //    }
    //}

}
