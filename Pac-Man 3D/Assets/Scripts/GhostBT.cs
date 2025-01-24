using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;
using UnityEngine.AI;

public class GhostBT : BTree
{
    public List<Transform> points;
    public float exitTime;

    public LayerMask player;

    public int point = 0;

    Vector3 initialPos;

    protected override Node SetupTree()
    {
        initialPos = transform.position;
        return new Selector(
            this,
            new List<Node>
            {
                new Sequence(
                    this,
                    new List<Node> { new OnRangeNode(this), new GoToTargetNode(this) }
                ),
                new Sequence(this, new List<Node> { new WaitNode(this), new PatrolNode(this) }),
            }
        );
    }

    public void ResetPosition()
    {
        GetComponent<NavMeshAgent>().Warp(initialPos);
        point = 0;
    }
}
