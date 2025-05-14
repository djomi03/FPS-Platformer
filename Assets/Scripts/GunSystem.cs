using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class GunSystem : MonoBehaviour
{

    public float damage = 1f;
    public float range = 100f;

    public bool canShoot = false;
    public string gunType = "";

    public Camera fpsCam;

    public float timeBetweenShooting, spread, timeBetweenShots;
    public float Shotgunspreadx, Shotgunspready;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;

    bool readyToShoot = true;

    public GameObject YellowBullet;
    public GameObject RedBullet;
    public GameObject PurpleBullet;
    public GameObject bulletSpawn;
    GameObject bullet;

    public Image UIActive;
    public Text AmmoActive;


    private void Update()
    {
        if (Input.GetMouseButton(0) && readyToShoot && canShoot)
        {
            Shoot();
        }

        if (gunType == "Yellow")
        {
            bullet = YellowBullet;
            timeBetweenShooting = 0.2f;
            timeBetweenShots = 0.2f;
            bullet.GetComponent<hit>().Gundamage = 1;
            magazineSize = 9;
        }
        if (gunType == "Red")
        {
            bullet = RedBullet;
            timeBetweenShooting = 0.5f;
            timeBetweenShots = 0.5f;
            bullet.GetComponent<hit>().Gundamage = 1;
            magazineSize = 4;
            Shotgunspreadx = 0.3f;
            Shotgunspready = 0.1f;
        }
        if (gunType == "Purple")
        {
            bullet = PurpleBullet;
            timeBetweenShooting = 0.8f;
            timeBetweenShots = 0.8f;
            bullet.GetComponent<hit>().Gundamage = 5;
            magazineSize = 5;
        }
    }
    private void Shoot()
    {
        readyToShoot = false;

        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);

        
        

        //RaycastHit hit;
        //if (Physics.Raycast(fpsCam.transform.position, direction, out hit))
        //{
        //    if (hit.collider.gameObject.tag == "Enemy")
        //    {
        //        turret Turret = hit.transform.GetComponent<turret>();

        //        if (Turret != null)
        //        {
        //            Turret.TakeDamage(damage);
        //            Debug.Log("Enemy hit");
        //        }
        //    }
        //}

        if (gunType == "Yellow")
        {
            
            GameObject instance = Instantiate(bullet, bulletSpawn.transform.position, Quaternion.identity);
            instance.GetComponent<Rigidbody>().AddForce(direction * 3000f);
        }
        if (gunType == "Red")
        {
            
            for (int i = 0; i < 10; i++)
            {
                x = Random.Range(-Shotgunspreadx, Shotgunspreadx);
                y = Random.Range(-Shotgunspready, Shotgunspready);
                Vector3 Shotgundirection = fpsCam.transform.forward + fpsCam.transform.right * x + fpsCam.transform.up * y; // zato sto shotgunspread x i y zavisi od scene a ne od kamere moram da saberem tako da zavisi od kamere
                Shotgundirection.Normalize();
                GameObject instance = Instantiate(bullet, bulletSpawn.transform.position, Quaternion.identity);
                instance.GetComponent<Rigidbody>().AddForce(Shotgundirection * 3000f);
            }
        }
        if (gunType == "Purple")
        {
            GameObject instance = Instantiate(bullet, bulletSpawn.transform.position, Quaternion.identity);
            instance.GetComponent<Rigidbody>().AddForce(direction * 3000f);
        }

        bulletsLeft--;
        bulletsShot--;
        kretanje Kr = this.GetComponent<kretanje>();

        Debug.Log(Kr.cardAmmoActive);
        Kr.cardAmmoActive--;
        AmmoActive.text = Kr.cardAmmoActive.ToString();
        Debug.Log(Kr.cardAmmoActive);

        if (Kr.cardAmmoActive <= 0)
        {
            Kr.CardStackTypeActive = "";
            Kr.cardAmmoActive = 0;
            Kr.cardCountActive = 0;
            Kr.CountActive.text = "";
            AmmoActive.text = "";
            gunType = Kr.CardStackTypeSecondary;
            Kr.Zamena();
        }

        Invoke("ResetShot", timeBetweenShooting);

        if (bulletsShot > 0 && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);


    }
    private void ResetShot()
    {
        readyToShoot = true;
    }

}
