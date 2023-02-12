using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    public bool isLimited = false;              //Cek input dibatasi. False = Input tidak dibatasi = Input masuk
    public KeyCode playerMoveLeft   = KeyCode.A;
    public KeyCode playerMoveRight  = KeyCode.D;
    public KeyCode interactKey      = KeyCode.F;
    public KeyCode returnKey        = KeyCode.Return;
    public KeyCode escapeKey        = KeyCode.Escape;
    public KeyCode tabKey           = KeyCode.Tab;

    private void Awake()
    {
        CreateInstance();
    }

    private void CreateInstance()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void ToogleLimiter()
    {
        isLimited = !isLimited;
    }

    public void ToogleLimiter(bool value)
    {
        isLimited = value;
    }
}

