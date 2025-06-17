using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour
{
    public void LoadLevel()
    {
        Debug.Log("Level change");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
