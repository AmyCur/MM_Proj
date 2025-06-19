using UnityEngine;
using AmyCurr.Settings;

public class LanguageButtonController : MonoBehaviour
{
    [SerializeField] languages targetLanguage;

    public void ChangeLanguage()
    {
        Settings.Language.ChangeLanguage(targetLanguage);
    }

}
