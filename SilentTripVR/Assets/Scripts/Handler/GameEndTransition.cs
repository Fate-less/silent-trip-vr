using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

public class GameEndTransition : MonoBehaviour
{
    public TextMeshProUGUI statsTMP;
    public GameObject blackscreen;
    [SerializeField] private int transitionDuration;
    private TaskHandler taskHandler;

    private void Start()
    {
        taskHandler = GameObject.Find("TaskHandler").GetComponent<TaskHandler>();
    }

    public void ShowResult()
    {
        blackscreen.GetComponent<Image>().color = Color.clear;
        if(taskHandler.totalTaskCompleted == 0)
        {
            statsTMP.text = "Kamu belum memulai interaksi sosial. Jangan ragu untuk memulai percakapan dan berinteraksi dengan orang di sekitarmu!";
        }
        else if (taskHandler.totalTaskCompleted == 1)
        {
            statsTMP.text = "Kamu mulai membuka diri dan mencoba berinteraksi. Teruslah berusaha untuk menjalin lebih banyak interaksi sosial!";
        }
        else if (taskHandler.totalTaskCompleted == 2)
        {
            statsTMP.text = "Interaksi sosialmu semakin baik! Kamu sudah dapat menjalin komunikasi dengan beberapa orang secara terbuka.";
        }
        else if (taskHandler.totalTaskCompleted == 3)
        {
            statsTMP.text = "Luar biasa! Kamu sudah berhasil menunjukkan kemampuan interaksi sosialmu dengan sangat baik.";
        }
        blackscreen.SetActive(true);
        StartCoroutine(blackscreen.GetComponent<FadeInImage>().ImageFade(blackscreen.GetComponent<Image>(), 1, transitionDuration));
        StartCoroutine(TextFadeInTransition());
    }

    IEnumerator TextFadeInTransition()
    {
        yield return new WaitForSeconds(transitionDuration + 1);
        statsTMP.gameObject.SetActive(true);
    }
}
