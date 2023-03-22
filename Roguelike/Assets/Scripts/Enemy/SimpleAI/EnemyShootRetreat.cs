using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyShootRetreat : MonoBehaviour
{

    public Transform target;

    public float minimunDistance = 4f;
    public float moveSpeed = 2f;

    public GameObject projectile;
    public float timeBetweenShots;
    public float nextShotTime;


    private void Update()
    {

        if(Time.time > nextShotTime)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            nextShotTime = Time.time + timeBetweenShots;
        }

        float distance = Vector2.Distance(transform.position, target.position);

        if (distance < minimunDistance)
        {
            Retreat();
        }
        else
        {
            // attack
        }
    }



    private void Retreat()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, -moveSpeed * Time.deltaTime);

    }

}
