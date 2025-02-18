﻿using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class RadiusParticles : MonoBehaviour
{
    private readonly float scalingFactor = 120f;
    
    [SerializeField] private Movement2 player;
    [SerializeField] private float particleScale = 1f;

    private ParticleSystem particles;
    
    private void Awake()
    {
        particles = GetComponent<ParticleSystem>();
        var temp = particles.main;
        temp.startSize = particleScale;
    }
    
    private void OnEnable()
    {
        StartCoroutine(KeepRadiusUpdated());
    }
    
    private IEnumerator KeepRadiusUpdated()
    {
        while (true)
        {
            var lastRadius = player.RadiusVector().magnitude;
            
            var shape = particles.shape;
            shape.radius = lastRadius;

            var emissionRate = particles.emission;
            emissionRate.rateOverTime = lastRadius * scalingFactor;
            
            yield return new WaitUntil(() => Math.Abs(lastRadius - player.RadiusVector().magnitude) > float.Epsilon);
        }
    }
}