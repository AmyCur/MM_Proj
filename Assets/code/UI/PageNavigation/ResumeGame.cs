using UnityEngine;
using MathsAndSome;
public class ResumeGame : MonoBehaviour
{
  // GameObject HUD;
  public void Resume(){
    Debug.Log("Resume");
    mas.player.GetPlayer().gameObject.GetComponent<PauseController>().UnPause();
  }
}
