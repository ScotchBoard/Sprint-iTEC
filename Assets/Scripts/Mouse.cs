using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    [SerializeField]
    private Texture2D cursorTexture;
    [SerializeField]
    private CursorMode cursorMode = CursorMode.Auto;
    [SerializeField]
    private Vector2 hotSpot = Vector2.zero;

    void Start()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }
}
