﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDefaultCursor : MonoBehaviour {

    public Texture2D cursorTexture;
    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = Vector2.zero;
	// Use this for initialization
	void Awake () {
		Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
	}

}


/*
    void OnMouseEnter()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }
*/
