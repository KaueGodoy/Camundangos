using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnTorchOnCollision : MonoBehaviour
{
    [SerializeField] private GameObject[] _torchArray;
    [SerializeField] private float _delayBetweenTorches = 0.3f; 

    private void Start()
    {
        foreach (GameObject torch in _torchArray)
        {
            Transform lamp = torch.transform.Find("Lamp");
            Transform particle = torch.transform.Find("Particle");

            if (lamp != null)
            {
                lamp.gameObject.SetActive(false);
            }

            if (particle != null)
            {
                particle.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        NewPlayerController player = collision.GetComponent<NewPlayerController>();

        if (player != null)
        {
            // Start the coroutine to turn on torches one by one
            StartCoroutine(ActivateTorchesOneByOne());
        }
    }

    private IEnumerator ActivateTorchesOneByOne()
    {
        foreach (GameObject torch in _torchArray)
        {
            Transform lamp = torch.transform.Find("Lamp");
            Transform particle = torch.transform.Find("Particle");

            if (lamp != null)
            {
                lamp.gameObject.SetActive(true);
            }

            if (particle != null)
            {
                particle.gameObject.SetActive(true);
            }

            // Wait for the specified delay before activating the next torch
            yield return new WaitForSeconds(_delayBetweenTorches);
        }
    }
}

