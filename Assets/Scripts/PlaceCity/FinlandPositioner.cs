﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinlandPositioner : MonoBehaviour
{
    [Tooltip("Relative distance from the bottom of the screen")]
    public float distanceFromBottom = .01f;


    void Start()
    {
        Transform t = GetComponent<Transform>();
        SpriteRenderer r = GetComponent<SpriteRenderer>();

        int cameraWidth = Camera.main.pixelWidth;
        int cameraHeight = Camera.main.pixelHeight;
        int cameraX = cameraWidth / 2;
        int cameraY = (int)(distanceFromBottom * cameraHeight);

        Vector3 bottomCenterInWorld = Camera.main.ScreenToWorldPoint(new Vector2(cameraX, cameraY));

        float x = bottomCenterInWorld.x;
        float y = bottomCenterInWorld.y + r.bounds.extents.y;

        t.position = new Vector3(x, y, t.position.z);
    }

}
