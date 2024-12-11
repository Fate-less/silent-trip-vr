using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskGenerator : MonoBehaviour
{
    public int taskSeed;
    public TaskList[] taskList;
    public void RandomSeed()
    {
        int randomValue = Random.Range(1, 3);
        if (randomValue == 1) { taskSeed = 123; }
        else if (randomValue == 2) { taskSeed = 321; }
        else { taskSeed = 231; }
    }

    public void GenerateSeed()
    {
        RandomSeed();
        if (taskSeed == 123)
        {
            taskList[0].TaskSetOne();
            taskList[1].TaskSetTwo();
            taskList[2].TaskSetThree();
        }
        else if (taskSeed == 321)
        {
            taskList[0].TaskSetThree();
            taskList[1].TaskSetTwo();
            taskList[2].TaskSetOne();
        }
        else
        {
            taskList[0].TaskSetTwo();
            taskList[1].TaskSetThree();
            taskList[2].TaskSetOne();
        }
        for (int i = 0; i < taskList.Length; i++)
        {
            taskList[i].DisplayTask();
        }
    }
}
