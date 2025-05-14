using TMPro;
using UnityEngine;

public class door : MonoBehaviour
{
    public bool DoorOpened = false;
    public Canvas Text;
    public TextMeshPro text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (DoorOpened == true)
        {
            Text.GetComponentInChildren<TextMeshProUGUI>().text = "UNLOCKED";
        }
    }
}
