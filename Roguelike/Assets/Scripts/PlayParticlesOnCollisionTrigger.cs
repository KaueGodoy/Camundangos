using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayParticlesOnCollisionTrigger : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] _particles;

    private void Start()
    {
        foreach (ParticleSystem particle in _particles)
        {
            Transform particleTransform = particle.gameObject.transform;

            if (particleTransform != null)
            {
                particleTransform.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        NewPlayerController player = collision.GetComponent<NewPlayerController>();

        if (player != null)
        {

            foreach (ParticleSystem particle in _particles)
            {
                Transform particleTransform = particle.gameObject.transform;

                if (particleTransform != null)
                {
                    particleTransform.gameObject.SetActive(true);
                }
            }
        }
    }
}
