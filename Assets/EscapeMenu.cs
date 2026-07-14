using UnityEngine;
using UnityEngine.InputSystem;


public class EscapeMenu : MonoBehaviour
{
    public GameObject sessionBrowser;

    bool abierto = true;
    bool sessionStarted = false;

    void Start()
    {
        // Arranca con el menú activo
        if (sessionBrowser != null)
            sessionBrowser.SetActive(true);

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Método público para que el código que inicia la sesión lo llame.
    // Ejemplo: FindObjectOfType<EscapeMenu>()?.OnSessionStarted();
    public void OnSessionStarted()
    {
        DeactivateMenu();
    }

    void DeactivateMenu()
    {
        abierto = false;
        sessionStarted = true;

        if (sessionBrowser != null)
            sessionBrowser.SetActive(false);

        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Keyboard.current == null) return;

        // Toggle con Escape
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            abierto = !abierto;
            if (sessionBrowser != null)
                sessionBrowser.SetActive(abierto);

            if (abierto)
            {
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Time.timeScale = 1f;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        // Si la sesión aún no se considera iniciada, intentamos desactivar automáticamente
        if (!sessionStarted && abierto)
        {
            // 1) Si ya existe un GameObject con tag "Player" asumimos que la partida arrancó
            if (GameObject.FindWithTag("Player") != null)
            {
                DeactivateMenu();
                return;
            }

            // 2) O si el jugador hace un input claro (clic izquierdo/tecla Enter/Space) después del arranque,
            //    lo consideramos inicio de sesión y ocultamos el menú.
            if (Time.timeSinceLevelLoad > 0.5f)
            {
                var mouse = Mouse.current;
                if (mouse != null && (mouse.leftButton.wasPressedThisFrame || mouse.rightButton.wasPressedThisFrame))
                {
                    DeactivateMenu();
                    return;
                }

                if (Keyboard.current.spaceKey.wasPressedThisFrame || Keyboard.current.enterKey.wasPressedThisFrame)
                {
                    DeactivateMenu();
                    return;
                }
            }
        }
    }
}

