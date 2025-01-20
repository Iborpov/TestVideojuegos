using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class PatrolNode : Node
{
    GhostBT ghostBT;
    UnityEngine.AI.NavMeshAgent agent;

    float exitTimePassed = 0f;
    float exitTime;
    int point;
    float timePassed = 0f;
    float time = 1f;

    public PatrolNode(BTree btree)
        : base(btree)
    {
        ghostBT = bTree as GhostBT;
        agent = ghostBT.transform.GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    public override NodeState Evaluate()
    {
        exitTime = ghostBT.exitTime;
        exitTimePassed += Time.deltaTime;

        if (exitTimePassed > exitTime)
        {
            agent.destination = ghostBT.points[point].transform.position;

            var distance = Vector2.Distance(
                new Vector2(ghostBT.transform.position.x, ghostBT.transform.position.z),
                new Vector2(
                    ghostBT.points[point].transform.position.x,
                    ghostBT.points[point].transform.position.z
                )
            );

            if (distance < .1f)
            {
                timePassed += Time.deltaTime;
                if (timePassed > time)
                {
                    timePassed = 0;
                    point++;
                    if (point >= 4)
                    {
                        point = 0;
                    }
                }
            }
        }

        state = NodeState.RUNNING;
        return state;
    }
}
