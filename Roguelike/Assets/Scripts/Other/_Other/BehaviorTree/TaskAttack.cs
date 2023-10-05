using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class TaskAttack : Node
{
    private Transform _lastTarget;

    public TaskAttack(Transform transform)
    {

    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");
        if (target != _lastTarget)
        {
            _lastTarget = target;
        }

        Debug.Log("Attacking");

        state = NodeState.Running;
        return state;
    }


}
