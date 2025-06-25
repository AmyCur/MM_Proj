using UnityEngine;
using AmyCurr.Settings;

public class TranslateText : MonoBehaviour
{
    [TextArea(8,20)]
    public string english, spanish, french, german;

    void Start()
    {
        Settings.Language.ChangeLanguage(Settings.Language.language);
    }
}
