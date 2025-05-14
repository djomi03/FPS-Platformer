using UnityEngine;

public class VisibilitySkripta : MonoBehaviour
{
    public GameObject bridge;
    private MeshRenderer[] rend;
    public Material[] materiali;

    private void OnTriggerEnter(Collider other)
    {
        rend = bridge.GetComponentsInChildren<MeshRenderer>();
        if (other.gameObject.name == "Player")
        {
            bridge.SetActive(true);
            for (int i = 0; i < 3; i++)
            {
                rend[i].material = materiali[1];
            }
        }
    }
}
