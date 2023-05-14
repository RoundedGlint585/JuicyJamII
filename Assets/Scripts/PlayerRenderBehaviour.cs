using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class PlayerRenderBehaviour : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    private Material material;
    private HealthBehaviour healthBehaviour;
    private ParticleSystem particleSystem;
    public float particleSystemStartOffset = -0.15f;//quite random honestly in local coordinates
    private ParticleSystem.ShapeModule editableShape;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        material = spriteRenderer.material;
        healthBehaviour = GetComponent<HealthBehaviour>();
        particleSystem = GetComponent<ParticleSystem>();
        particleSystem.Stop();
        editableShape = particleSystem.shape;
        Vector3 position = particleSystem.shape.position; //local
        position.y = particleSystemStartOffset;
        editableShape.position = position;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthBehaviour.IsInvincible())
        {
            float loadingProgress = healthBehaviour.GetInvincibilityProgress();
            material.SetFloat("_LoadingProgress", loadingProgress);
            if (particleSystem.isStopped)
            {
                particleSystem.Play();
            }
            Vector3 position = particleSystem.shape.position; //local
            position.y = particleSystemStartOffset + MathF.Abs(particleSystemStartOffset) * 2.0f * loadingProgress;
            editableShape.position = position;
        }
        else
        {
            if (!particleSystem.isStopped)
            {
                particleSystem.Stop();
            }
        }
    }
}
