using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHandler : MonoBehaviour
{
    public List<GameObject> npcList;
    public float duration;
    public float fadeDuration;
    public float cooldownDuration;

    private int currentIndex = 0;
    private int nextIndex = 0;
    private int prevIndex = 0;

    private void Start()
    {
        CycleNPC();
    }

    // Update is called once per frame
    void Update()
    {
        duration = duration + Time.deltaTime;
        if (duration >= cooldownDuration)
        {
            duration = 0;
            CycleNPC();
        }


        for (int i = 0; i < 10; i++)
        {
            if (!npcList[i].activeInHierarchy)
            {
                npcList[i].GetComponent<StareDetection>().isStaring = false;
                npcList[i].GetComponent<StareDetection>().enabled = false;
            }
        }
    }

    public void CycleNPC()
    {
        nextIndex = currentIndex + 3;
        //disable
        for (; prevIndex < currentIndex; prevIndex++)
        {
            StartCoroutine(disableNPC(npcList[prevIndex]));
            StartCoroutine(npcList[prevIndex].GetComponent<FadeInNPC>().SpriteFade(npcList[prevIndex].GetComponent<SpriteRenderer>(), 0, fadeDuration));
            StartCoroutine(npcList[prevIndex].transform.GetChild(1).GetComponent<FadeInNPC>().SpriteFade(npcList[prevIndex].transform.GetChild(1).GetComponent<SpriteRenderer>(), 0, fadeDuration));
            StartCoroutine(enableEye(npcList[prevIndex].GetComponent<StareDetection>()));
        }
        //enable
        for (; currentIndex < nextIndex; currentIndex++)
        {
            Debug.Log(currentIndex);
            npcList[currentIndex].SetActive(true);
            StartCoroutine(npcList[currentIndex].GetComponent<FadeInNPC>().SpriteFade(npcList[currentIndex].GetComponent<SpriteRenderer>(), 1, fadeDuration));
            StartCoroutine(npcList[currentIndex].transform.GetChild(1).GetComponent<FadeInNPC>().SpriteFade(npcList[currentIndex].transform.GetChild(1).GetComponent<SpriteRenderer>(), 1, fadeDuration));
            StartCoroutine(enableEye(npcList[currentIndex].GetComponent<StareDetection>()));
        }
        if (currentIndex > 10)
        {
            currentIndex = 0;
            nextIndex = 0;
            prevIndex = 0;
        }
    }

    IEnumerator enableEye(StareDetection stareScript)
    {
        yield return new WaitForSeconds(2f);
        stareScript.enabled = true;
    }

    IEnumerator disableNPC(GameObject npc)
    {
        yield return new WaitForSeconds(2f);
        npc.GetComponent<StareDetection>().enabled = false;
        npc.SetActive(false);
    }

    private void CustomAppear(int i)
    {
        npcList[i].SetActive(true);
        npcList[i].GetComponent<FadeInNPC>().SpriteFade(npcList[i].GetComponent<SpriteRenderer>(), 1, fadeDuration);
        npcList[i].transform.GetChild(1).GetComponent<FadeInNPC>().SpriteFade(npcList[i].transform.GetChild(1).GetComponent<SpriteRenderer>(), 1, fadeDuration);
        StartCoroutine(enableEye(npcList[i].GetComponent<StareDetection>()));
    }

    private void CustomFade(int i)
    {
        StartCoroutine(disableNPC(npcList[i]));
        npcList[i].GetComponent<FadeInNPC>().SpriteFade(npcList[i].GetComponent<SpriteRenderer>(), 0, fadeDuration);
        npcList[i].transform.GetChild(1).GetComponent<FadeInNPC>().SpriteFade(npcList[i].transform.GetChild(1).GetComponent<SpriteRenderer>(), 0, fadeDuration);
        StartCoroutine(enableEye(npcList[i].GetComponent<StareDetection>()));
    }
}
