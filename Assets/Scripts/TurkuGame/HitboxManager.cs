﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxManager : MonoBehaviour { 

    private Hitbox[] hitboxes;
    public TurkuAnimation turkuAnimation;
    public TurkuManager manager;
    public int difficulty;
    private bool gameOver = false;
    void Start()
    {
        hitboxes = new Hitbox[transform.childCount];
        int i = 0;
        foreach (Transform child in transform)
        {
            hitboxes[i] = child.GetComponent<Hitbox>();
            i++;
        }
    }

    void Update()
    {
        if (this.Active() > Mathf.Min(40,difficulty) && !gameOver)
        {
            this.gameOver = true;
            this.Win();
        }
    }

    private int Active()
    {
        int i = 0;
        foreach(Hitbox h in this.hitboxes)
        {
            if (h.getActive()) i++;
        }
        return i;
    }

    private int MaxAmount()
    {
        return this.hitboxes.Length;
    }

    private void Win()
    {
        StartCoroutine(EndGame());
        Debug.Log("VOITIT PELIN!");
    }

    IEnumerator EndGame()
    {
        turkuAnimation.StartAnimation();
        yield return new WaitForSecondsRealtime(3);
        manager.EndMinigame(true);
    }
}
