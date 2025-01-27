using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;
using UnityEngine.AI;

public class GhostBT : BTree
{
    public List<Transform> points;
    public float exitTime;
    public LayerMask player;
    public bool start = true;

    Vector3 initialPos;

    protected override Node SetupTree()
    {
        initialPos = transform.position;
        return new Selector(
            this,
            new List<Node>
            {
                new StartNode(this),
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
        start = true;
        GetComponent<NavMeshAgent>().Warp(initialPos);
    }
}
