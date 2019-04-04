﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour {

    private WebServiceScript webScript;
    private Text playerInfo;

    void Start() {
        webScript = FindObjectOfType<WebServiceScript> ();
        playerInfo = GameObject.Find("PlayerInfo").GetComponent<Text>();
        updatePlayerInfo();
    }

    private void updatePlayerInfo() {
        string info = "Username: "+ PlayerPrefs.GetString("username")+"\n";
        info += "ID: " + PlayerPrefs.GetString("_id") + "\n";
        info += "Token: " + PlayerPrefs.GetString("token") + "\n";
        info += "HighScore: " + PlayerPrefs.GetInt("highScore") + "\n";
        info += "Registered: " + ((PlayerPrefs.GetInt("registered") == 1) ? "yes" : "no")+ "\n";
        info += "Rank: " + PlayerPrefs.GetInt("rank") + "\n";

        playerInfo.text = info;
    }

    public void DeleteAllPlayerPrefs() {
        PlayerPrefs.DeleteAll();
        updatePlayerInfo();
    }

    public void DeleteID() {
        PlayerPrefs.DeleteKey("_id");
        updatePlayerInfo();
    }

    public void DeleteUsername() {
        PlayerPrefs.DeleteKey("username");
        updatePlayerInfo();
    }

    public void DeleteToken() {
        PlayerPrefs.DeleteKey("token");
        updatePlayerInfo();
    }

    public void DeleteHighScore() {
        PlayerPrefs.DeleteKey("highScore");
        updatePlayerInfo();
    }

    public void DeleteRegistered() {
        PlayerPrefs.DeleteKey("registered");
        updatePlayerInfo();
    }

    public void increaseHighScore() {
        PlayerPrefs.SetInt("highScore", PlayerPrefs.GetInt("highScore")+10);
        updatePlayerInfo();
    }

    public void decreaseHighScore() {
        PlayerPrefs.SetInt("highScore",PlayerPrefs.GetInt("highScore")-10);
        updatePlayerInfo();
    }
    
    public void syncHS() {
        webScript.UpdateHighscore (PlayerPrefs.GetString("_id"), PlayerPrefs.GetInt("highScore"));
    }

    public void loadMainMenu() {
        SceneManager.LoadScene ("MainMenu", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync ("Settings");
    }

    
}