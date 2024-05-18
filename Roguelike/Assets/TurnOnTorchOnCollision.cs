using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnTorchOnCollision : MonoBehaviour
{
    [SerializeField] private GameObject[] _torchArray;

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
        }
    }
}
