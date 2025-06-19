using UnityEngine;
using TMPro;

public class TranslateText : MonoBehaviour
{
  TMP_Text text;

  void Start(){
    text = GetComponent<TMP_Text>();
  }
}
