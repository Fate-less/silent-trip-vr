using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LLMUnitySamples;

public class TaskHandler : MonoBehaviour
{
    private TaskGenerator taskGenerator;
    public Sprite taskCompletedSprite;
    public StareDetection stareDetection;

    private void Start()
    {
        taskGenerator = GetComponent<TaskGenerator>();
    }

    public void CheckAnswer(string answer, TaskList taskList)
    {
        if(taskList.currentTaskSet == 1)
        {
            if(answer == "1")
            {
                taskList.Task.RemoveAt(0);
                taskList.spriteRenderer[0].sprite = taskCompletedSprite;
            }
            else if(answer == "2")
            {
                taskList.Task.RemoveAt(1);
                taskList.spriteRenderer[1].sprite = taskCompletedSprite;
            }
            else if(answer == "3")
            {
                taskList.Task.RemoveAt(2);
                taskList.spriteRenderer[2].sprite = taskCompletedSprite;
            }
        }
        else if(taskList.currentTaskSet == 2)
        {
            if (answer == "4")
            {
                taskList.Task.RemoveAt(0);
                taskList.spriteRenderer[0].sprite = taskCompletedSprite;
            }
            else if (answer == "5")
            {
                taskList.Task.RemoveAt(1);
                taskList.spriteRenderer[1].sprite = taskCompletedSprite;
            }
            else if (answer == "6")
            {
                taskList.Task.RemoveAt(2);
                taskList.spriteRenderer[2].sprite = taskCompletedSprite;
            }
        }
        else if (taskList.currentTaskSet == 3)
        {
            if (answer == "7")
            {
                taskList.Task.RemoveAt(0);
                taskList.spriteRenderer[0].sprite = taskCompletedSprite;
            }
            else if (answer == "8")
            {
                taskList.Task.RemoveAt(1);
                taskList.spriteRenderer[1].sprite = taskCompletedSprite;
            }
            else if (answer == "9")
            {
                taskList.Task.RemoveAt(2);
                taskList.spriteRenderer[2].sprite = taskCompletedSprite;
            }
        }
    }
}
