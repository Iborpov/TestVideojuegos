using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class PatrolNode : Node
{
    GhostBT ghostBT;
    UnityEngine.AI.NavMeshAgent agent;

    public PatrolNode(BTree btree)
        : base(btree)
    {
        ghostBT = bTree as GhostBT;
        agent = ghostBT.transform.GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    public override NodeState Evaluate()
    {
        agent.destination = ghostBT.points[0].transform.position;

        Debug.Log("Position actual " + agent.transform.position);
        Debug.Log("Position final " + ghostBT.points[0].transform.position);
        var distance = Vector2.Distance(
            new Vector2(ghostBT.transform.position.x, ghostBT.transform.position.z),
            new Vector2(
                ghostBT.points[0].transform.position.x,
                ghostBT.points[0].transform.position.z
            )
        );
        Debug.Log(distance);
        if (distance < .1f)
        {
            Debug.Log("Ya estoy en el punto 0");
        }

        state = NodeState.RUNNING;
        return state;
    }
}
