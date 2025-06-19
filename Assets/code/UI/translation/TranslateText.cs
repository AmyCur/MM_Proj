using UnityEngine;
using AmyCurr.Settings;

public class TranslateText : MonoBehaviour
{
    public string english;
    public string spanish;
    public string french;
    public string german;

    void Start()
    {
        Settings.Language.ChangeLanguage(Settings.Language.language);
    }
}
