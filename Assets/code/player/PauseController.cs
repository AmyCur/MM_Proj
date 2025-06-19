using UnityEngine;
using Magical;

public class PauseController : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [HideInInspector] public bool paused;
    bool shouldPause => magic.key.down(keys.pause);

    void Pause()
    {
        Debug.Log("Pause");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GetComponent<PlayerController>().canRotate = false;
        paused = true;
        pauseMenu.SetActive(true);
    }

    public void UnPause()
    {
        Debug.Log("UnPause");
        paused = false;
        Cursor.lockState = CursorLockMode.Locked;
        GetComponent<PlayerController>().canRotate = true;
        Cursor.visible = false;
        pauseMenu.SetActive(false);
    }

    void Start()
    {
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (shouldPause)
            if (paused) UnPause();
            else Pause();
                                           
}}
