using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class PatrolNode : Node
{
    GhostBT ghostBT;
    UnityEngine.AI.NavMeshAgent agent;

    int point;

    public PatrolNode(BTree btree)
        : base(btree)
    {
        ghostBT = bTree as GhostBT;
        agent = ghostBT.transform.GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    public override NodeState Evaluate()
    {
        Debug.Log("Patrol Node ----------------------------------");
        point = ghostBT.point + 1;
        Debug.Log(point + "p");
        agent.destination = ghostBT.points[point - 1].transform.position;

        var distance = Vector2.Distance(
            new Vector2(ghostBT.transform.position.x, ghostBT.transform.position.z),
            new Vector2(
                ghostBT.points[point - 1].transform.position.x,
                ghostBT.points[point - 1].transform.position.z
            )
        );
        Debug.Log("Distance: " + distance);
        bTree.SetData("distance", distance);
        if (distance < .1f)
        {
            ghostBT.point++;
            if (point >= 5)
            {
                ghostBT.point = 1;
                point = 1;
            }
            state = NodeState.SUCCESS;
        }
        else
        {
            state = NodeState.RUNNING;
        }

        return state;
    }
}
