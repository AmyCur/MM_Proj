using UnityEngine;
using Magical;

public class PauseController : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [HideInInspector] public bool paused;
    [SerializeField] GameObject HUD;
    GameObject menu;
    bool shouldPause => magic.key.down(keys.pause);

    void Pause()
    {
        menu = GameObject.Instantiate(pauseMenu, Vector3.zero, Quaternion.identity);
        menu.transform.SetParent(HUD.transform, true);
        // menu.transform.position = new(transform.position.x+648.2353f+550f, transform.position.y + 182f, 0);

        Debug.Log("Pause");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GetComponent<PlayerController>().canRotate = false;
        paused = true;
    }

    public void UnPause()
    {
        Destroy(menu);
        Debug.Log("UnPause");
        paused = false;
        Cursor.lockState = CursorLockMode.Locked;
        GetComponent<PlayerController>().canRotate = true;
        Cursor.visible = false;
    }


    void Update()
    {
        if (shouldPause)
            if (paused) UnPause();
            else Pause();
    }
}
