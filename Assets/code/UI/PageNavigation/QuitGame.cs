using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitGame : MonoBehaviour
{
  public void Quit(){
        SceneManager.LoadScene(0);
  }
}
