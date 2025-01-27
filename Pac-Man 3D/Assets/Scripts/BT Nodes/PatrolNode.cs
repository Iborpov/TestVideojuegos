using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class PatrolNode : Node
{
    GhostBT ghostBT;
    UnityEngine.AI.NavMeshAgent agent;

    int point = 0;

    public PatrolNode(BTree btree)
        : base(btree)
    {
        ghostBT = bTree as GhostBT;
        agent = ghostBT.transform.GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    public override NodeState Evaluate()
    {
        //Calcula la distancia entre el punto A y el B
        var distance = Vector2.Distance(
            new Vector2(ghostBT.transform.position.x, ghostBT.transform.position.z),
            new Vector2(
                ghostBT.points[point].transform.position.x,
                ghostBT.points[point].transform.position.z
            )
        );

        if (distance < .1f) //Si la distancia del punto A a punto B es 0
        {
            bTree.SetData("OnPoint", true); //Establece que si ha llegado

            point++;
            if (point >= 4) //Si el punto es igual o mayor a 4 se reescribe al 0
            {
                point = 0;
            }
            state = NodeState.SUCCESS;
        }
        else
        {
            agent.destination = ghostBT.points[point].transform.position; //Envia al agent al siguiente punto
            bTree.SetData("OnPoint", false); //Establece que aun no ha llegado
            state = NodeState.RUNNING;
        }

        return state;
    }
}
