using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class GhostBT : BTree
{
    public List<Transform> points;
    public float exitTime;

    public LayerMask player;

    Transform position;

    protected override Node SetupTree()
    {
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

    private void Awake()
    {
        position = this.gameObject.transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.transform.position = position.position;
        }
    }
}
