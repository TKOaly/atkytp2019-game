﻿using UnityEngine;

public class TurkuAnimation : MonoBehaviour
{
    public ParticleSystem trail;
    public ParticleSystem particles;
    public Transform turku;
    public SpriteRenderer turkuOld;
    private bool started = false;
    private Vector3 direction = new Vector3(-1, -2, 0);
    private Vector3 rotation = new Vector3(0, 0, 3);

    public void StartAnimation()
    {
        var emissionTrail = trail.emission;
        var emission = particles.emission;
        emissionTrail.enabled = false;
        emission.enabled = false;
        turkuOld.enabled = false;
        started = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (started)
        {
            direction *= 1+Time.deltaTime;
            turku.Translate(direction * Time.deltaTime);
            turku.Rotate(rotation*Time.deltaTime);
            if (turku.position.y < -1000) started = false;
        }
    }
}
