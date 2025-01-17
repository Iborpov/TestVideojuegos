using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class GhostBT : BTree
{
    public List<Transform> points;

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
                new PatrolNode(this),
            }
        );
    }
}
