using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class OnRangeNode : Node
{
    GhostBT ghostBT;
    UnityEngine.AI.NavMeshAgent agent;

    public OnRangeNode(BTree btree)
        : base(btree)
    {
        ghostBT = bTree as GhostBT;
        agent = ghostBT.transform.GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    public override NodeState Evaluate()
    {
        state = NodeState.FAILURE;
        return state;
    }
}
