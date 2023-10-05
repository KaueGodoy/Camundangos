using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckEnemyInAttackRange : Node
{
    private Transform _transform;

    public CheckEnemyInAttackRange(Transform transform)
    {
        _transform = transform;
    }

    public override NodeState Evaluate()
    {
        object t = GetData("target");
        if (t == null)
        {
            state = NodeState.Failure;
            return state;
        }

        Transform target = (Transform)t;
        if (Vector2.Distance(_transform.position, target.position) <= EnemyBT.attackRange)
        {
            // animation 
            Debug.Log("Attack range");
            state = NodeState.Success;
            return state;

        }

        state = NodeState.Failure;
        return state;
    }

}
