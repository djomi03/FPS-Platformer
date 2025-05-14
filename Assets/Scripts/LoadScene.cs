using UnityEngine;
using UnityEngine.SceneManagement;  // Needed to load scenes
using TMPro;  // Make sure to include this to work with TextMeshPro

public class LoadScene : MonoBehaviour
{
    public void SceneLoad()
    {
        // Load the specified scene by name
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
