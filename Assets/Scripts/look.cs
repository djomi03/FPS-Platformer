using UnityEngine;

public class look : MonoBehaviour
{
    public float sensitivityX = 2f;  // Horizontal sensitivity
    public float sensitivityY = 2f;  // Vertical sensitivity
    public Transform playerBody;     // Reference to the player body (for rotating the body)

    private float xRotation = 0f;    // To keep track of the camera's vertical rotation
    public GameObject bullet;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Locks the cursor to the center of the screen
        Cursor.visible = false; // Hides the cursor
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivityX;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivityY;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limit vertical rotation to avoid flipping

        // Apply the rotation to the camera
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotate the player body (the whole character) based on the horizontal mouse movement
        playerBody.Rotate(Vector3.up * mouseX);  // Rotate player body horizontally

        //if (Input.GetMouseButtonDown(0))
        //{
        //    GameObject instance = Instantiate(bullet, this.transform.position, this.transform.rotation);
        //    instance.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * 2000);
            //RaycastHit hit;
            //if (Physics.Raycast(transform.position, transform.forward, out hit))
            //{
            //    if (hit.collider.gameObject.tag == "Enemy")
            //    {
            //        if (hit.collider.transform.parent)
            //        {
            //            print(hit.collider.transform.parent.gameObject.name + " unistavanje");
            //            Destroy(hit.collider.transform.parent.gameObject);
            //        }
            //        else 
            //        {
            //            print(hit.collider.gameObject.name + " unistavanje");
            //            Destroy(hit.collider.transform.gameObject);
            //        }
                    
            //    }
            //}
        //}


    }
}
