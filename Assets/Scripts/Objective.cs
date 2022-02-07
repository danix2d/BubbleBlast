using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    public int startMoves;
    public IntVariable moves;
    public GameEvent youWin;
    public GameEvent gameOver;

    public List<ObjectiveList> objectives = new List<ObjectiveList>();

    private bool finish;
    private int finishedObjectives;

    private void OnEnable()
    {
        moves.Value = startMoves;

        for (int i = 0; i < objectives.Count; i++)
        {
            objectives[i].element.colected = 0;
            objectives[i].element.toColect =  objectives[i].elementToCollect;
            objectives[i].objectiveIsMet = false;
        }
    }

    private void Update()
    {
        Status();
    }


    private void Status()
    {
        if(finish) return;

        finishedObjectives = 0;

        for (int i = 0; i < objectives.Count; i++)
        {
            if(objectives[i].objectiveIsMet) 
            {
                finishedObjectives++;
                continue;
            }

            if(objectives[i].element.colected >= objectives[i].elementToCollect)
            {
                objectives[i].objectiveIsMet = true;
            }
        }

        if(finishedObjectives == objectives.Count)
        {
            youWin.Raise();
            finish = true;
        }

        if(moves.Value <= 0)
        {
            gameOver.Raise();
            finish = true;
        }
        
    }
}
