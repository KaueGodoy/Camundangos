using UnityEngine;
using BehaviorTree;
using System.Collections.Generic;

public class EnemyBT : BehaviorTree.Tree
{
    public Transform[] waypoints;

    public static float speed = 5f;
    public static float fovRange = 1f;
    public static float attackRange = 0.5f;

    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckEnemyInAttackRange(transform),
                new TaskAttack(transform),
            }),
            new Sequence(new List<Node>
            {
                new CheckEnemyInFOVRange(transform),
                new TaskGoToTarget(transform),
            }),

            new TaskPatrol(transform, waypoints),

        });

        return root;
    }
}
