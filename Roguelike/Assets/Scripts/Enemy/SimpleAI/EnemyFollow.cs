using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform target;

    public float minimunDistance = 10f;
    public float moveSpeed = 2f;


    private void Update()
    {
        float distance = Vector2.Distance(transform.position, target.position);

        if (distance < minimunDistance)
        {
            Chase();
        }
        else
        {
            // attack
        }
    }

    private void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }

}
