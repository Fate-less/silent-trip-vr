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
            if(answer == "1")
            {
                taskList.Task.RemoveAt(0);
                checklist[0].sprite = taskCompletedSprite;
            }
            else if(answer == "2")
            {
                int num;
                if(taskList.Task.Count < 2) { num = 0; }
                else { num = taskList.Task.Count - 2; }
                taskList.Task.RemoveAt(num);
                checklist[1].sprite = taskCompletedSprite;
            }
            else if(answer == "3")
            {
                taskList.Task.RemoveAt(taskList.Task.Count - 1);
                checklist[2].sprite = taskCompletedSprite;
            }
        }
        else if(taskList.currentTaskSet == 2)
        {
            if (answer == "4")
            {
                taskList.Task.RemoveAt(0);
                checklist[0].sprite = taskCompletedSprite;
            }
            else if (answer == "5")
            {
                int num;
                if (taskList.Task.Count < 2) { num = 0; }
                else { num = taskList.Task.Count - 2; }
                taskList.Task.RemoveAt(num);
                checklist[1].sprite = taskCompletedSprite;
            }
            else if (answer == "6")
            {
                taskList.Task.RemoveAt(taskList.Task.Count - 1);
                checklist[2].sprite = taskCompletedSprite;
            }
        }
        else if (taskList.currentTaskSet == 3)
        {
            if (answer == "7")
            {
                taskList.Task.RemoveAt(0);
                checklist[0].sprite = taskCompletedSprite;
            }
            else if (answer == "8")
            {
                int num;
                if (taskList.Task.Count < 2) { num = 0; }
                else { num = taskList.Task.Count - 2; }
                taskList.Task.RemoveAt(num);
                checklist[1].sprite = taskCompletedSprite;
            }
            else if (answer == "9")
            {
                taskList.Task.RemoveAt(taskList.Task.Count - 1);
                checklist[2].sprite = taskCompletedSprite;
            }
        }
        if (taskList.Task.Count == 0)
        {
            stareDetection.allTaskDone = true;
            totalTaskCompleted++;
        }
    }
}
