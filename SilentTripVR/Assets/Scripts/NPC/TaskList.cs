using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskList : MonoBehaviour
{
    public List<string> Task;
    [HideInInspector] public int currentTaskSet;
    private TaskHandler taskHandler;

    private void Start()
    {
        taskHandler = GameObject.Find("TaskHandler").GetComponent<TaskHandler>();
    }
    public void DisplayTask()
    {
        for (int i = 0; i < 3; i++)
        {
            taskHandler.taskTMP[i].text = Task[i];
        }
    }
    public void TaskSetOne()
    {
        currentTaskSet = 1;
        Task.Add("Greet the person");
        Task.Add("Comment on the weather today to someone");
        Task.Add("Ask for their opinion about the train");
    }

    public void TaskSetTwo()
    {
        currentTaskSet = 2;
        Task.Add("Introduce yourself");
        Task.Add("Ask if they often take this train");
        Task.Add("Comment on the atmosphere inside the train at the moment");
    }

    public void TaskSetThree()
    {
        currentTaskSet = 3;
        Task.Add("Ask the person's name");
        Task.Add("Ask the person about the purpose of their trip");
        Task.Add("Give the person a compliment");
    }

    public void RefreshTask()
    {
        Task.Clear();
    }
}
