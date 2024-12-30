using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LLMUnitySamples;
using UnityEngine.UI;
using TMPro;

public class TaskHandler : MonoBehaviour
{
    private TaskGenerator taskGenerator;
    public Sprite taskCompletedSprite;
    public Sprite taskUncompletedSprite;
    public StareDetection stareDetection;
    public GameObject taskUI;
    public TextMeshProUGUI[] taskTMP;
    public Image[] checklist;
    [HideInInspector] public float totalTaskCompleted;

    private void Start()
    {
        taskGenerator = GetComponent<TaskGenerator>();
    }

    public void CheckAnswer(string answer, TaskList taskList)
    {
        if(taskList.currentTaskSet == 1)
        {
            if(answer == "1" && !taskList.firstTaskCleared)
            {
                taskList.Task.RemoveAt(0);
                checklist[0].sprite = taskCompletedSprite;
                taskList.firstTaskCleared = true;
            }
            else if(answer == "2" && !taskList.secondTaskCleared)
            {
                int num;
                if(taskList.Task.Count < 2) { num = 0; }
                else { num = taskList.Task.Count - 2; }
                taskList.Task.RemoveAt(num);
                checklist[1].sprite = taskCompletedSprite;
                taskList.secondTaskCleared = true;
            }
            else if(answer == "3" && !taskList.thirdTaskCleared)
            {
                taskList.Task.RemoveAt(taskList.Task.Count - 1);
                checklist[2].sprite = taskCompletedSprite;
                taskList.thirdTaskCleared = true;
            }
        }
        else if(taskList.currentTaskSet == 2)
        {
            if (answer == "4" && !taskList.firstTaskCleared)
            {
                taskList.Task.RemoveAt(0);
                checklist[0].sprite = taskCompletedSprite;
                taskList.firstTaskCleared = true;
            }
            else if (answer == "5" && !taskList.secondTaskCleared)
            {
                int num;
                if (taskList.Task.Count < 2) { num = 0; }
                else { num = taskList.Task.Count - 2; }
                taskList.Task.RemoveAt(num);
                checklist[1].sprite = taskCompletedSprite;
                taskList.secondTaskCleared = true;
            }
            else if (answer == "6" && !taskList.thirdTaskCleared)
            {
                taskList.Task.RemoveAt(taskList.Task.Count - 1);
                checklist[2].sprite = taskCompletedSprite;
                taskList.thirdTaskCleared = true;
            }
        }
        else if (taskList.currentTaskSet == 3)
        {
            if (answer == "7" && !taskList.firstTaskCleared)
            {
                taskList.Task.RemoveAt(0);
                checklist[0].sprite = taskCompletedSprite;
                taskList.firstTaskCleared = true;
            }
            else if (answer == "8" && !taskList.secondTaskCleared)
            {
                int num;
                if (taskList.Task.Count < 2) { num = 0; }
                else { num = taskList.Task.Count - 2; }
                taskList.Task.RemoveAt(num);
                checklist[1].sprite = taskCompletedSprite;
                taskList.secondTaskCleared = true;
            }
            else if (answer == "9" && !taskList.thirdTaskCleared)
            {
                taskList.Task.RemoveAt(taskList.Task.Count - 1);
                checklist[2].sprite = taskCompletedSprite;
                taskList.thirdTaskCleared = true;
            }
        }
        if (taskList.Task.Count == 0)
        {
            stareDetection.allTaskDone = true;
            totalTaskCompleted++;
        }
    }
}
