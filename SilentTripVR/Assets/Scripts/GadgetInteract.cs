using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GadgetInteract : MonoBehaviour
{
    public bool isOpen = false;
    public GameObject gadget;
    public EdgeCameraMovement playerFP;
    public bool gadgetCooldown = true;
    public GadgetFixCam gadgetCam;
    public DelayTransition delayTransition;
    public CameraZoom cameraZoom;
    public Image gadgetIcon;
    public Animator gadgetAnim;
    public AudioHandler audioHandler;
    public NoddingScript nodScript;
    public EventCondition eventManager;

    [SerializeField] float gadgetCooldownTime = 60f;
    [SerializeField] float buttonCooldownTime = 5f;
    private void Update()
    {
        gadgetCooldownTime = gadgetCooldownTime - Time.deltaTime;
        gadgetIcon.fillAmount = 1 - gadgetCooldownTime / 60f;
        if(gadgetCooldownTime <= 0)
        {
            gadgetCooldown = false;
        }
    }

    public void GadgetButton()
    {
        if (!gadgetCooldown && !nodScript.isNodding && !nodScript.isShaking && !eventManager.isAsking)
        {
            if (isOpen)
            {
                if (!LeanTween.isTweening(gadget))
                {
                    CloseGadget();
                }
            }
            else
            {
                if (!LeanTween.isTweening(gadget))
                {
                    delayTransition.homeScreen.SetActive(true);
                    if(SceneManager.GetActiveScene().name == "PlaneGame")
                    {
                        gadgetCam.PlaneFixCameraOpen();
                    }
                    else
                    {
                        gadgetCam.FixCameraOpen();
                    }
                    gadget.SetActive(true);
                    playerFP.enabled = false;
                    isOpen = true;
                    gadgetCooldown = true;
                    gadgetCooldownTime = 5f;
                    if (SceneManager.GetActiveScene().name == "TrainGame")
                    {
                        gadgetAnim.Play("pullOutPhone");
                    }
                    StartCoroutine(delayTransition.delayOpen());
                }
            }
        }
    }

    IEnumerator AnimationDelay()
    {
        yield return new WaitForSeconds(1f);
        if (gadget.activeInHierarchy)
        {
            gadget.SetActive(false);
            playerFP.enabled = true;
        }
    }

    public void CloseGadget()
    {
        if (SceneManager.GetActiveScene().name == "TrainGame")
        {
            gadgetAnim.Play("pullInPhone");
        }
        gadgetCooldown = true;
        gadgetCooldownTime = 60f;
        audioHandler.PlaySighSound();
        StartCoroutine(delayOneSec());
    }

    IEnumerator delayOneSec()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(delayTransition.currentScreen);
        delayTransition.homeScreen.SetActive(false);
        delayTransition.currentScreen = null;
        delayTransition.screenState = "none";
        if (SceneManager.GetActiveScene().name == "PlaneGame")
        {
            gadgetCam.PlaneFixCameraClose();
        }
        else
        {
            gadgetCam.FixCameraClose();
        }
        isOpen = false;
        StartCoroutine(AnimationDelay());
    }
}
