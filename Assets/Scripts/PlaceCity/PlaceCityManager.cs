﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceCityManager : MonoBehaviour {
    // GameObjects representing the cities
    public Transform[] locations;
    public SpriteRenderer map;
    public float radius = 1f;
    public int delayAfterMinigameEndsInSeconds = 2;
    public Text organisationText;
    private DataController dataController;
    private GameObject targetCity;
    private Dictionary<string, string> organisationsByCities;
    private bool gameIsOver = false;



    public int difficulty = 1;



    void Start() {
        dataController = FindObjectOfType<DataController>();
        GetComponent<DifficultyAdjuster>().Initialize(difficulty);

        // No need to show the positions to the players of the production build
        if (Debug.isDebugBuild) {
            for (int i = 0; i < locations.Length; i++) {
                locations[i].GetComponent<CircleCollider2D>().radius = 2 * radius;
            }
        }

        organisationsByCities = new Dictionary<string, string>(){
                {"Helsinki", "TKO-aly"},
                {"Turku", "Asteriski" },
                {"Tampere", "Luuppi" },
                {"Jyväskylä", "Linkki" },
                {"Kuopio", "Serveri" },
                {"Oulu", "Blanko"}
            };

        targetCity = locations[((int)Random.Range(0f, 6f))].gameObject;
        this.SetOnlyCityActive(targetCity);
        organisationText.text = organisationsByCities[targetCity.name];
    }


    private void SetOnlyCityActive(GameObject city) {
        for (int i = 0; i < locations.Length; i++) {
            if (locations[i].gameObject != city) {
                locations[i].gameObject.SetActive(false);
            }
        }
    }


    public void handleCityInteraction(GameObject city) {
        if (city != null) {
            if (city == targetCity) {
                winMinigame();
            } else {
                loseMinigame();
            }

        } else {
            loseMinigame();
        }

    }

    public void winMinigame() {
        if (gameIsOver) {
            return;
        }
        StartCoroutine(EndMinigame(true));
    }

    public void loseMinigame() {
        /*
         * Check for the following situation:  
         * player has clicked the correct city, but the timer runs out. Should the timer stop?
         *
         */
        if (gameIsOver) {
            return;
        }
        StartCoroutine(EndMinigame(false));
    }


    private IEnumerator EndMinigame(bool win) {

        Color statusColor = win
            ? Color.green
            : Color.red;

        gameIsOver = true;

        TimeProgress timerScript = FindObjectOfType<TimeProgress>();
        timerScript.StopTimerProgression();

        targetCity.GetComponent<InformationDisplayer>().DisplayOnMap(statusColor);
        yield return new WaitForSeconds(delayAfterMinigameEndsInSeconds);
        //varmaan hyvä idea lopulta (jos SceneManagerCamera renderöi jotain pelien välissä)
        //activateOnlyCamera("SceneManagerCamera");
        dataController.MinigameEnd(win, win ? 10 : 0);
    }
}