using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using System.Security.Cryptography;

public class TaskGoToTarget : Node
{
    private Transform _transform;

    public TaskGoToTarget(Transform transform)
    {
        _transform = transform;
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");

        if (target == null)
        {
            state = NodeState.Failure;
            return state;
        }


        if (Vector2.Distance(_transform.position, target.position) <= EnemyBT.fovRange * 20)
        {
            _transform.position = Vector2.MoveTowards(
                _transform.position, target.position, EnemyBT.speed * Time.deltaTime);
            Debug.Log("Chasing");
        }
        state = NodeState.Running;
        return state;
    }
}
