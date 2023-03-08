using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public Vector2 Direction { get; set; }
    public float Range { get; set; }
    public float Speed { get; set; }
    public int Damage { get; set; }

    private void Start()
    {
        Speed = 50f;
        GetComponent<Rigidbody2D>().AddForce(Direction * Speed);
    }
}
