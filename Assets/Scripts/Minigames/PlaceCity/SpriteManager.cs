﻿using UnityEngine;

public class SpriteManager : MonoBehaviour {
    // in order of ascending difficulty
    public Sprite[] mapSprites;
    private SpriteRenderer finlandRenderer;

    
    void Awake() {
        finlandRenderer = GetComponent<SpriteRenderer>();
        finlandRenderer.sprite = mapSprites[0];
    }

    // map difficulty need not be the difficulty of the game
    // the decision will be made in the DifficultyAdjuster script
    public void ChangeSprite(int mapDifficulty) {
        int idx = Mathf.Min(mapSprites.Length - 1, mapDifficulty);
        finlandRenderer.sprite = mapSprites[idx];
    }


    public void Flip(bool horizontally, bool vertically) {
        finlandRenderer.flipX = horizontally;
        finlandRenderer.flipY = vertically;

        FlipCities(horizontally, vertically);
    }

    // The positions of the cities do not update automatically
    private void FlipCities(bool hor, bool vert) {
        Bounds bounds = finlandRenderer.bounds;

        Transform[] cityTransforms = GetComponentsInChildren<Transform>();
        for (int i = 0; i < cityTransforms.Length; i++) {
            if (!cityTransforms[i].gameObject.activeSelf) {
                continue;
            }
            Vector3 center = bounds.center;
            Vector3 additionVector = new Vector3(0, 0, 0);
            if (hor) {
                // vektori (bounds.center.x - cityTransforms[i], 0) on vektori kaupungin sijainnista
                // spriten pystysuuntaiseen halkaisijaan. Tämä kahdesti lisättynä antaa 
                // vektorin haluttu_piste - nykyinen_piste, missä haluttu piste siis pystysuuntaisen halkaisijan
                // suhteen peilattu piste.
                additionVector.x += 2 * (bounds.center.x - cityTransforms[i].position.x);
            }

            if (vert) {
                // hyvin sama logiikka kuin ylemmässä laskussa
                additionVector.y += 2 * (bounds.center.y - cityTransforms[i].position.y);
            }

            cityTransforms[i].position += additionVector;
        }


    }


    // Currently never called
    // Does hold the cities in correct positions...?
    public void Rotate(float degrees)
    {
        transform.Rotate(new Vector3(0, 0, degrees));
    }



}
