using UnityEngine;
using UnityEngine.InputSystem;


public class EscapeMenu : MonoBehaviour
{
    public GameObject sessionBrowser;

    bool abierto = false;

    void Start()
    {
        sessionBrowser.SetActive(false);
    }

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            abierto = !abierto;
            sessionBrowser.SetActive(abierto);

            Cursor.lockState = abierto ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = abierto;
        }
    }
}

