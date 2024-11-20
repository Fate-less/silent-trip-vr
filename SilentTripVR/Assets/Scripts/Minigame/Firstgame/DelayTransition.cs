using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DelayTransition : MonoBehaviour
{
    public float setDelay = 4f;
    public GameObject homeScreen;
    public GameObject firstScreen;
    public GameObject secondScreen;
    public GameObject thirdScreen;
    public GameObject rewardScreen;
    public GadgetInteract gadgetInteract;
    public MinigameManager minigameManager;
    public minigameTutorial minigameTutorial;
    public TextMeshProUGUI minigameInstructionTMP;

    public string screenState = "none";
    public GameObject currentScreen = null;

    private void Start()
    {
        homeScreen.SetActive(false);
    }

    public IEnumerator delayOpen()
    {
        yield return new WaitForSeconds(setDelay);
        homeScreen.SetActive(true);
        minigameTutorial.OpenMinigameTutorial();
        NextScreen();
    }

    public void NextScreen()
    {
        if (screenState == "first")
        {
            Destroy(currentScreen);
            currentScreen = Instantiate(secondScreen, homeScreen.transform.position, gameObject.transform.rotation, gameObject.transform);
            screenState = "second";
            minigameManager.playerSanity.anxiety = minigameManager.playerSanity.anxiety + 20;
            minigameInstructionTMP.text = "Click when the line overlaps";
        }
        else if (screenState == "second")
        {
            Destroy(currentScreen);
            currentScreen = Instantiate(thirdScreen, homeScreen.transform.position, gameObject.transform.rotation, gameObject.transform);
            screenState = "third";
            minigameManager.playerSanity.anxiety = minigameManager.playerSanity.anxiety + 20;
            minigameInstructionTMP.text = "Put the ball on the hole";
        }
        else if (screenState == "third")
        {
            Destroy(currentScreen);
            currentScreen = Instantiate(rewardScreen, homeScreen.transform.position, gameObject.transform.rotation, gameObject.transform);
            screenState = "reward";
            minigameManager.playerSanity.anxiety = minigameManager.playerSanity.anxiety + 20;
            minigameInstructionTMP.text = "";
        }
        else if (screenState == "reward")
        {
            Destroy(currentScreen);
            gadgetInteract.CloseGadget();
        }
        else if(currentScreen == null)
        {
            currentScreen = Instantiate(firstScreen, homeScreen.transform.position, gameObject.transform.rotation, gameObject.transform);
            screenState = "first";
            minigameInstructionTMP.text = "Avoid the cars";
        }

        /*gadget pesawat
stare detection pesawat
fatigue bar and mechanics
plane events

4209

yang di train voice pas jawab event dibuat gelagapan
yang di pesawat mulai lancar ngomongny*/
    }
}
