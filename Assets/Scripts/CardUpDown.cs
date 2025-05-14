using UnityEngine;

public class CardUpDown : MonoBehaviour
{
    float visina = 0.5f;
    Vector3 startingLocation = new Vector3(0,0,0);
    int dir = 0;

    private void Start()
    {
        startingLocation = this.gameObject.transform.position;
        dir = 1; // up
        Debug.Log("this.gameObject.transform.position  " + this.gameObject.transform.position);
        Debug.Log("this.transform.position  " + this.transform.position);
        Debug.Log("startingLocation " + startingLocation);
        Debug.Log("startingLocation + 0.5 " + startingLocation.y + 5);
    }
    // Update is called once per frame
    void Update()
    {

        this.gameObject.transform.Translate(0f, 0.8f * dir * Time.deltaTime, 0f);
        if (this.gameObject.transform.position.y >= startingLocation.y + 0.5)
        {
            dir = -1;
        }
        if (this.gameObject.transform.position.y <= startingLocation.y)
        {
            dir = 1;
        }
    }
}
