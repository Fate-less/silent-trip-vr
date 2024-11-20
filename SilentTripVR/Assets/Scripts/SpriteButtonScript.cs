using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteButtonScript : MonoBehaviour
{
    // The color when the button is not pressed
    //public Color normalColor = Color.white;
    // The color when the button is pressed
    //public Color pressedColor = Color.gray;
    public NoddingScript nodScript;
    public string buttonType;
    public EventCondition eventCondition;
    public subtitleScript subtitleScript;

    private Material buttonMaterial;
    private bool isPressed = false;
    private AudioSource audioSource;
    private drinkInteract drinkInteract;

    void Start()
    {
        // Get the material of the button
        buttonMaterial = GetComponent<Renderer>().material;
        audioSource = GetComponent<AudioSource>();
        drinkInteract = GameObject.Find("Minigame Manager").GetComponent<drinkInteract>();
        subtitleScript = GetComponent<subtitleScript>();
        // Set the initial color
        //buttonMaterial.color = normalColor;
    }

    void Update()
    {
        //if (isPressed)
        //{
        //    // Change to pressed color when the button is pressed
        //    buttonMaterial.color = pressedColor;
        //}
        //else
        //{
        //    // Change to normal color when the button is not pressed
        //    buttonMaterial.color = normalColor;
        //}
    }

    void OnMouseDown()
    {
        // This function is called when the mouse button is pressed
        //isPressed = true;
        Debug.Log("Button Pressed!");
        // Add your button pressed logic here
        if (buttonType == "nod button")
        {
            nodScript.HandleNodding();
            nodScript.stopNodding();
            subtitleScript.subtitleOn();
        }
        if (buttonType == "headshake button")
        {
            nodScript.HandleHeadshake();
            nodScript.stopShaking();
            subtitleScript.subtitleOn();
        }
        if (buttonType == "pramugari nod button")
        {
            
            nodScript.HandleNodding();
            nodScript.stopNodding();
            drinkInteract.waterDrank -= 1;
            subtitleScript.subtitleOnPramugari();
        }
        if (buttonType == "pramugari headshake button")
        {
            nodScript.HandleHeadshake();
            nodScript.stopShaking();
            drinkInteract.waterDrank -= 1;
            subtitleScript.subtitleOnPramugari();
        }
        AudioSource.PlayClipAtPoint(audioSource.clip, this.gameObject.transform.position);
        StartCoroutine(delayStopAskingActive());
    }

    void OnMouseUp()
    {
        // This function is called when the mouse button is released
        //isPressed = false;
    }

    IEnumerator delayStopAskingActive()
    {
        yield return new WaitForSeconds(4f);
        eventCondition.isAsking = false;
        transform.parent.gameObject.SetActive(false);
        eventCondition.cameraScript.enabled = true;
    }
}
