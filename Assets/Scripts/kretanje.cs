using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;
using System;

public class kretanje : MonoBehaviour
{
    public GameObject bridge;
    public Material material1, material2;
    public door Door;
    public Animator DoorAnimator;
    Vector3 spawn = new Vector3(0, 1.5f, 0);
    float smerX, smerZ;
    public float brzina = 5f;
    Vector3 movementDirection;
    public Rigidbody rb;
    bool jumped = false;
    private MeshRenderer[] rend;
    bool key = false;
    [HideInInspector] public bool invulnerable = false;
    public Image UI;
    public Text CountActive;
    public Text CountSecondary;
    public Text AmmoActive;

    public Image UIActive;
    public Image UISecondary;
    Sprite UITEMP;

    string CountTemp;

    int cardCountSecond = 0;
    int cardCountTemp;

    int cardAmmoTemp;

    string CardStackTypeTemp;

    public Sprite[] gunSprites;

    Vector3 moveDirection;

    [HideInInspector] public int cardAmmoActive;
    [HideInInspector] public int cardAmmoSecond;
    [HideInInspector] public string CardStackTypeActive = "";
    [HideInInspector] public string CardStackTypeSecondary;
    [HideInInspector] public int cardCountActive = 0;
    [HideInInspector] public float groundCheckDistance = 0.5f;
    CharacterController controller;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GunSystem Gun = this.transform.GetComponent<GunSystem>();
        bridge.SetActive(false);
        rend = bridge.GetComponentsInChildren<MeshRenderer>();
        for (int i = 0; i < 3; i++)
        {
            rend[i].material = material1;
        }
        controller = GetComponent<CharacterController>();
    }
    public void Zamena()
    {
        if (cardCountSecond > 0)
        {

            UITEMP = UIActive.sprite;
            UIActive.sprite = UISecondary.sprite;
            UISecondary.sprite = UITEMP;

            CountTemp = CountActive.text;
            CountActive.text = CountSecondary.text;
            CountSecondary.text = CountTemp;

            cardCountTemp = cardCountActive;
            cardCountActive = cardCountSecond;
            cardCountSecond = cardCountTemp;

            CardStackTypeTemp = CardStackTypeActive;
            CardStackTypeActive = CardStackTypeSecondary;
            CardStackTypeSecondary = CardStackTypeTemp;

            cardAmmoTemp = cardAmmoActive;
            cardAmmoActive = cardAmmoSecond;
            cardAmmoSecond = cardAmmoTemp;

            AmmoActive.text = cardAmmoActive.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        GunSystem Gun = this.transform.GetComponent<GunSystem>();
        
        if (CardStackTypeActive == "Yellow")
        {
            Gun.gunType = "Yellow";
        }
        if (CardStackTypeActive == "Red")
        {
            Gun.gunType = "Red";
        }
        if (CardStackTypeActive == "Purple")
        {
            Gun.gunType = "Purple";
        }
        if (cardCountActive == 0)
        {
            UIActive.sprite = null;
            UIActive.color = new Color(255, 255, 255, 0);
        }
        if (cardCountSecond == 0)
        {
            UISecondary.sprite = null;
            UISecondary.color = new Color(255, 255, 255, 0);
        }

        smerX = Input.GetAxisRaw("Horizontal");
        smerZ = Input.GetAxisRaw("Vertical");
        Vector3 pravac = new Vector3(smerX, 0, smerZ).normalized;


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!jumped)
            {
                rb.linearVelocity = new Vector3(0, 0, 0);
                rb.AddForce(0, 400f, 0);
                jumped = true;
            }
        }
        groundCheckDistance = (GetComponent<CapsuleCollider>().height / 2) + 0.1f;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, groundCheckDistance))
        {
            jumped = false;
               
        }
        else if (Physics.Raycast(transform.position + new Vector3(0.4f, 0, 0), -transform.up, out hit, groundCheckDistance))
        {
            jumped = false;
        }
        else if (Physics.Raycast(transform.position + new Vector3(-0.4f, 0, 0), -transform.up, out hit, groundCheckDistance))
        {
            jumped = false;
        }
        else if (Physics.Raycast(transform.position + new Vector3(0, 0, 0.4f), -transform.up, out hit, groundCheckDistance))
        {
            jumped = false;
        }
        else if (Physics.Raycast(transform.position + new Vector3(0, 0, -0.4f), -transform.up, out hit, groundCheckDistance))
        {
            jumped = false;
        }
        else { jumped = true; }

        //if (!jumped)
        //    rb.linearDamping = 5;
        //else
        //    rb.linearDamping = 0;

        if (Input.GetMouseButtonDown(1))
        {
            if (cardCountActive > 0 && CardStackTypeActive == "Yellow")
            {
                cardCountActive--;
                CountActive.text = cardCountActive.ToString();
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
                rb.linearVelocity = new Vector3(0, 0, 0);
                rb.linearDamping = 0;
                StopAllCoroutines();
                invulnerable = false;
                rb.AddForce(0,550,0);
                if (cardCountActive == 0)
                {
                    if (cardCountSecond > 0)
                    {
                        UIActive.sprite = UISecondary.sprite;
                        CountActive.text = CountSecondary.text;
                        cardCountActive = cardCountSecond;
                        CardStackTypeActive = CardStackTypeSecondary;
                        cardAmmoActive = cardAmmoSecond;
                        AmmoActive.text = cardAmmoActive.ToString();

                        UISecondary.sprite = null;
                        UISecondary.color = new Color(255, 255, 255, 0);
                        CountSecondary.text = "";
                        cardCountSecond = 0;
                        CardStackTypeSecondary = "";
                        CountSecondary.text = "";

                        return;
                    }
                    UIActive.sprite = null;
                    UIActive.color = new Color(255, 255, 255, 0);
                    Gun.gunType = "";
                    CardStackTypeActive = "";
                    cardAmmoActive = 0;
                    AmmoActive.text = "";
                    CountActive.text = "";
                    return;
                }
                
            }
            if (cardCountActive > 0 && CardStackTypeActive == "Red")
            {
                cardCountActive--;
                CountActive.text = cardCountActive.ToString();
                Vector3 cm = Camera.main.transform.forward;
                StopAllCoroutines();
                rb.linearVelocity = new Vector3(0, 0, 0);
                rb.AddForce(cm * 60 , ForceMode.Impulse);
                StartCoroutine(AddDrag());
                if (cardCountActive == 0)
                {
                    if (cardCountSecond > 0)
                    {
                        UIActive.sprite = UISecondary.sprite;
                        CountActive.text = CountSecondary.text;
                        cardCountActive = cardCountSecond;
                        CardStackTypeActive = CardStackTypeSecondary;
                        cardAmmoActive = cardAmmoSecond;
                        AmmoActive.text = cardAmmoActive.ToString();

                        UISecondary.sprite = null;
                        UISecondary.color = new Color(255, 255, 255, 0);
                        CountSecondary.text = "";
                        cardCountSecond = 0;
                        CardStackTypeSecondary = "";
                        CountSecondary.text = "";

                        return;
                    }
                    UIActive.sprite = null;
                    UIActive.color = new Color(255, 255, 255, 0);
                    Gun.gunType = "";
                    CardStackTypeActive = "";
                    cardAmmoActive = 0;
                    AmmoActive.text = "";
                    CountActive.text = "";
                    return;
                }
            }
            if (cardCountActive > 0 && CardStackTypeActive == "Purple")
            {
                cardCountActive--;
                CountActive.text = cardCountActive.ToString();
                Vector3 cm = this.gameObject.transform.forward;
                //Vector3 cm = Camera.main.transform.forward;
                cm.y = 0;
                StopAllCoroutines();
                rb.linearVelocity = new Vector3(0, 0, 0);
                rb.AddForce(cm * 40, ForceMode.Impulse);
                StartCoroutine(AddDrag());
                if (cardCountActive == 0)
                {
                    if (cardCountSecond > 0)
                    {
                        UIActive.sprite = UISecondary.sprite;
                        CountActive.text = CountSecondary.text;
                        cardCountActive = cardCountSecond;
                        CardStackTypeActive = CardStackTypeSecondary;
                        cardAmmoActive = cardAmmoSecond;
                        AmmoActive.text = cardAmmoActive.ToString();

                        UISecondary.sprite = null;
                        UISecondary.color = new Color(255, 255, 255, 0);
                        CountSecondary.text = "";
                        cardCountSecond = 0;
                        CardStackTypeSecondary = "";
                        CountSecondary.text = "";

                        return;
                    }
                    UIActive.sprite = null;
                    UIActive.color = new Color(255, 255, 255, 0);
                    Gun.gunType = "";
                    CardStackTypeActive = "";
                    cardAmmoActive = 0;
                    AmmoActive.text = "";
                    CountActive.text = "";
                    return;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        { 
            Application.LoadLevel(Application.loadedLevel);
            Time.timeScale = 1;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Zamena();
        }

        transform.Translate(pravac * brzina * Time.deltaTime);

    }


    IEnumerator AddDrag()
    {
        invulnerable = true;
        float current_drag = 0;
        //float dragScale = 1f;

        while (current_drag < 10)
        {
            current_drag += Time.unscaledDeltaTime * 7;
            rb.linearDamping = current_drag;
            //dragScale += 0.03f;
            yield return null; // čeka sledeći frame

            //current_drag += Time.deltaTime * 10;
            //rb.linearDamping = current_drag;
            //yield return new WaitForSecondsRealtime(0.002f);
        }
        invulnerable = false;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.linearDamping = 0;
    }

    //private void OnCollisionStay(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Ground")
    //    {
    //        jumped = false;
    //    }

    //}
    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Ground")
    //    {
    //        jumped = true;
    //    }
    //}
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "DeadZone")
        {
            this.transform.position = spawn;
            this.GetComponent<Rigidbody>().isKinematic = true;
            this.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            if(!invulnerable)
                rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == "Enemy" || other.gameObject.tag == "YellowEnemy" || other.gameObject.tag == "RedEnemy" || other.gameObject.tag == "PurpleEnemy") && invulnerable && other is CapsuleCollider)
        {
            turret Turret = other.transform.GetComponent<turret>();
            Turret.TakeDamage(1000);
        }
        if (other.gameObject.tag == "Water")
        {
            brzina += 10f;
        }
        if (other.gameObject.tag == "Obstacle")
        {
            Time.timeScale = 0;
        }
        
        if (other.gameObject.name == "rust_key")
        {
            key = true;
            Destroy(other.gameObject);
        }
        if (other.gameObject.name == "Finish")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (other.gameObject.tag == "Card")
        {
            GunSystem Gun = this.transform.GetComponent<GunSystem>();
            Gun.canShoot = true;
            Gun.gunType = "Yellow";
            if (cardCountActive == 0)
            {
                cardCountActive++;
                CountActive.text = cardCountActive.ToString();
                CardStackTypeActive = "Yellow";
                cardAmmoActive = 9;
                AmmoActive.text = cardAmmoActive.ToString();
                Destroy(other.gameObject);
                UIActive.sprite = gunSprites[0];
                UIActive.color = new Color(255, 255, 255, 255);
            }
            else if (cardCountActive > 0 && cardCountActive < 4)
            {
                if (CardStackTypeActive == "Yellow")
                {
                    if (cardCountActive < 3)
                    {
                        cardCountActive++;
                        CountActive.text = cardCountActive.ToString();
                        cardAmmoActive += 9;
                        AmmoActive.text = cardAmmoActive.ToString();
                        Destroy(other.gameObject);
                        UIActive.sprite = gunSprites[0];
                        UIActive.color = new Color(255, 255, 255, 255);
                    }
                }
                else
                {
                    if (cardCountSecond == 0)
                    {
                        cardCountSecond++;
                        CountSecondary.text = cardCountSecond.ToString();
                        CardStackTypeSecondary = "Yellow";
                        cardAmmoSecond = 9;
                        Destroy(other.gameObject);
                        UISecondary.sprite = gunSprites[0];
                        UISecondary.color = new Color(255, 255, 255, 255);
                        Zamena();
                    }
                    else if (cardCountSecond > 0 && cardCountSecond < 3 && CardStackTypeSecondary == "Yellow")
                    {
                        //cardCountSecond = 0;
                        cardCountSecond++;
                        CountSecondary.text = cardCountSecond.ToString();
                        cardAmmoSecond += 9;
                        CardStackTypeSecondary = "Yellow";
                        Destroy(other.gameObject);
                        UISecondary.sprite = gunSprites[0];
                        UISecondary.color = new Color(255, 255, 255, 255);
                        Zamena();
                    }
                    else if (cardCountSecond > 0 && cardCountSecond <= 3 && CardStackTypeSecondary != "Yellow")
                    {
                        cardCountSecond = 0;
                        cardCountSecond++;
                        CountSecondary.text = cardCountSecond.ToString();
                        cardAmmoSecond = 9;
                        CardStackTypeSecondary = "Yellow";
                        Destroy(other.gameObject);
                        UISecondary.sprite = gunSprites[0];
                        UISecondary.color = new Color(255, 255, 255, 255);
                        Zamena();
                    }
                }

            }
        }
        if (other.gameObject.tag == "Card 2")
        {
            GunSystem Gun = this.transform.GetComponent<GunSystem>();
            Gun.canShoot = true;
            Gun.gunType = "Red";
            if (cardCountActive == 0)
            {
                cardCountActive++;
                CountActive.text = cardCountActive.ToString();
                CardStackTypeActive = "Red";
                cardAmmoActive = 4;
                AmmoActive.text = cardAmmoActive.ToString();
                Destroy(other.gameObject);
                UIActive.sprite = gunSprites[1];
                UIActive.color = new Color(255, 255, 255, 255);
            }
            else if (cardCountActive > 0 && cardCountActive < 4)
            {
                if (CardStackTypeActive == "Red")
                {
                    if (cardCountActive < 3)
                    {
                        cardCountActive++;
                        CountActive.text = cardCountActive.ToString();
                        cardAmmoActive += 4;
                        AmmoActive.text = cardAmmoActive.ToString();
                        Destroy(other.gameObject);
                        UIActive.sprite = gunSprites[1];
                        UIActive.color = new Color(255, 255, 255, 255);
                    }
                }
                else
                {
                    if (cardCountSecond == 0)
                    {
                        cardCountSecond++;
                        CountSecondary.text = cardCountSecond.ToString();
                        CardStackTypeSecondary = "Red";
                        cardAmmoSecond = 4;
                        Destroy(other.gameObject);
                        UISecondary.sprite = gunSprites[1];
                        UISecondary.color = new Color(255, 255, 255, 255);
                        Zamena();
                    }
                    else if (cardCountSecond > 0 && cardCountSecond < 3 && CardStackTypeSecondary == "Red")
                    {
                        //cardCountSecond = 0;
                        cardCountSecond++;
                        CountSecondary.text = cardCountSecond.ToString();
                        cardAmmoSecond += 4;
                        CardStackTypeSecondary = "Red";
                        Destroy(other.gameObject);
                        UISecondary.sprite = gunSprites[1];
                        UISecondary.color = new Color(255, 255, 255, 255);
                        Zamena();
                    }
                    else if (cardCountSecond > 0 && cardCountSecond <= 3 && CardStackTypeSecondary != "Red")
                    {
                        cardCountSecond = 0;
                        cardCountSecond++;
                        CountSecondary.text = cardCountSecond.ToString();
                        cardAmmoSecond = 4;
                        CardStackTypeSecondary = "Red";
                        Destroy(other.gameObject);
                        UISecondary.sprite = gunSprites[1];
                        UISecondary.color = new Color(255, 255, 255, 255);
                        Zamena();
                    }
                }

            }

        }
        if (other.gameObject.tag == "Card 3")
        {
            GunSystem Gun = this.transform.GetComponent<GunSystem>();
            Gun.canShoot = true;
            Gun.gunType = "Purple";
            if (cardCountActive == 0)
            {
                cardCountActive++;
                CountActive.text = cardCountActive.ToString();
                CardStackTypeActive = "Purple";
                cardAmmoActive = 5;
                AmmoActive.text = cardAmmoActive.ToString();
                Destroy(other.gameObject);
                UIActive.sprite = gunSprites[2];
                UIActive.color = new Color(255, 255, 255, 255);
            }
            else if (cardCountActive > 0 && cardCountActive < 4)
            {
                if (CardStackTypeActive == "Purple")
                {
                    if (cardCountActive < 3)
                    {
                        cardCountActive++;
                        CountActive.text = cardCountActive.ToString();
                        cardAmmoActive += 5;
                        AmmoActive.text = cardAmmoActive.ToString();
                        Destroy(other.gameObject);
                        UIActive.sprite = gunSprites[2];
                        UIActive.color = new Color(255, 255, 255, 255);
                    }
                }
                else
                {
                    if (cardCountSecond == 0)
                    {
                        cardCountSecond++;
                        CountSecondary.text = cardCountSecond.ToString();
                        CardStackTypeSecondary = "Purple";
                        cardAmmoSecond = 5;
                        Destroy(other.gameObject);
                        UISecondary.sprite = gunSprites[2];
                        UISecondary.color = new Color(255, 255, 255, 255);
                        Zamena();
                    }
                    else if (cardCountSecond > 0 && cardCountSecond < 3 && CardStackTypeSecondary == "Purple")
                    {
                        //cardCountSecond = 0;
                        cardCountSecond++;
                        CountSecondary.text = cardCountSecond.ToString();
                        cardAmmoSecond += 5;
                        CardStackTypeSecondary = "Purple";
                        Destroy(other.gameObject);
                        UISecondary.sprite = gunSprites[2];
                        UISecondary.color = new Color(255, 255, 255, 255);
                        Zamena();
                    }
                    else if (cardCountSecond > 0 && cardCountSecond <= 3 && CardStackTypeSecondary != "Purple")
                    {
                        cardCountSecond = 0;
                        cardCountSecond++;
                        CountSecondary.text = cardCountSecond.ToString();
                        cardAmmoSecond = 5;
                        CardStackTypeSecondary = "Purple";
                        Destroy(other.gameObject);
                        UISecondary.sprite = gunSprites[2];
                        UISecondary.color = new Color(255, 255, 255, 255);
                        Zamena();
                    }
                }

            }

        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "BreakableWall" && invulnerable)
        {
            Destroy(other.transform.parent.gameObject);
        }
        if (other.gameObject.name == "Visibility")
        {
            bridge.SetActive(true);
            for (int i = 0; i < 3; i++)
            {
                rend[i].material = material2;
            }
        }
        if (other.gameObject.name == "Locked_door")
        {
            if (Input.GetKeyDown(KeyCode.E) && key == true)
            {
                Door.DoorOpened = true;
                DoorAnimator.SetTrigger("Interact");
            }
        }
    }
private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
            brzina -= 10f;
        }
        if (other.gameObject.name == "Visibility")
        {
            for (int i = 0; i < 3; i++)
            {
                rend[i].material = material1;
            }
        }
    }
}
