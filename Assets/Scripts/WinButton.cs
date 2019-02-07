﻿using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class WinButton : MonoBehaviour
{
    private DataController dataController;
    private void Start() {
        dataController = FindObjectOfType<DataController>();
    }
    // Start is called before the first frame update
    public void EndMinigame()
    {
        //level = filterOdd(level);
        //SceneManager.LoadScene(level);
        dataController.SetRoundEndStatus(true);
        dataController.SetWinStatus(true);
        dataController.AddCurrentScore(10);
    }

    private string filterOdd(string str) {
        char[] arr = str.ToCharArray();

        arr = Array.FindAll<char>(arr, (c => (char.IsLetterOrDigit(c) 
                                  || char.IsWhiteSpace(c) 
                                  || c == '-')));
        str = new string(arr);
        return str;
    }
}