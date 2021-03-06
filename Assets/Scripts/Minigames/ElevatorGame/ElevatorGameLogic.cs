﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ElevatorGameLogic : MonoBehaviour, IMinigameEnder {
    
    private GameObject[] jumpmanList;
    private GameObject[] borders;
    
    private MinigameLogic miniGameLogic; 
    private TimeProgress timer;
    private GameObject brokenRope;
    private GameObject supportRope;
    private GameObject infoText;
    private GameObject instructions;
    
    public bool forceDownActive;
    private float damage;
    private bool endedGame;

    void Start() {
        
        endedGame = false;
        damage = 0;

        miniGameLogic = GameObject.FindObjectOfType<MinigameLogic>();
        borders = GameObject.FindGameObjectsWithTag("Border");
        jumpmanList =  GameObject.FindGameObjectsWithTag("Jumpman");
        supportRope = GameObject.Find("SupportRope");
        infoText = GameObject.Find("InfoText");

        brokenRope = GameObject.Find("BrokenSupportRopes");
        brokenRope.SetActive(false);
        
        timer = FindObjectOfType<TimeProgress>();
        timer.SetTime(15);
    }

    //Win with one damage (one forcedown), but option to add more.
    public void AddDamage(float DMG){
        damage += DMG;
        if(damage >= 1 && !endedGame){
            endedGame = true;
            this.WinMinigame();
        }
    }

    public void WinMinigame() {
        foreach (GameObject jumper in jumpmanList){
            jumper.GetComponent<EventTrigger>().enabled = false;
            jumper.GetComponent<JumpmanLogic>().ChangeToScared();
        }
        brokenRope.SetActive(true);
        supportRope.SetActive(false);
        GameObject.FindObjectOfType<ElevatorShaftMove>().move = false;
        
        this.AddRigidBodies();
        EndGame(true);
    }

    private void AddRigidBodies(){
        foreach(GameObject border in borders){
                border.GetComponent<AddRigidBody>().AddRB();
        }
        GameObject.Find("ElevatorDoors").GetComponent<AddRigidBody>().AddRB();
    }
    
    public void LoseMinigame() {
        infoText.SetActive(true);
        infoText.GetComponent<Text>().text = "TIME OVER"; 
        
        endedGame = true;
        foreach (GameObject jumper in jumpmanList){
            jumper.GetComponent<EventTrigger>().enabled = false;
        }
        EndGame(false);
    }

    public void OnTimerEnd() {
        this.LoseMinigame();
    }

    public async void EndGame(bool won) {
        timer.StopTimerProgression();
        await new WaitForSecondsRealtime(3);
        GameManager.EndMinigame(won);
    }

    public void PressForceDownButton(){
        forceDownActive = true;
        foreach (GameObject jumper in jumpmanList){
            jumper.GetComponent<JumpmanLogic>().ForceDown();
        }
    }
}
