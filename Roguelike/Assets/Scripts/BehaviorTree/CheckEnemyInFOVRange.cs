using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class CheckEnemyInFOVRange : Node
{
    private static int _enemyLayerMask = 1 << 9; // 6 = layer mask number in the inpector

    private Transform _transform;

    public CheckEnemyInFOVRange(Transform transform)
    {
        _transform = transform;
    }

    public override NodeState Evaluate()
    {
        object t = GetData("target");

        if (t == null)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(
                _transform.position, EnemyBT.fovRange, _enemyLayerMask);

            if (colliders.Length > 0)
            {
                parent.parent.SetData("target", colliders[0].transform);
                // animation
                Debug.Log("In range");

                state = NodeState.Success;
                return state;
            }

            state = NodeState.Failure;
            return state;
        }

        state = NodeState.Running;
        return state;


    }

}
