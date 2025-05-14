using UnityEngine;

public class bridgeController : MonoBehaviour
{
    private MeshRenderer[] rend;
    public Material material1, material2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        this.gameObject.SetActive(false);
        rend = this.gameObject.GetComponentsInChildren<MeshRenderer>();
        for (int i = 0; i < 3; i++)
        {
            rend[i].material = material1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
