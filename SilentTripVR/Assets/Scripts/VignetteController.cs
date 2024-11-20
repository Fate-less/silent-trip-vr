using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class VignetteController : MonoBehaviour
{
    public PostProcessVolume postProcessVolume; // Assign this in the inspector
    public HeartbeatVFX heartBeat;
    [SerializeField] Vignette vignette;
    [SerializeField] DepthOfField DoF;
    [SerializeField] LensDistortion distort;

    private playerSanity playerSanity;
    [SerializeField] float heartbeatCooldown = 5;

    private void Start()
    {
        playerSanity = gameObject.GetComponent<playerSanity>();
        vignette = postProcessVolume.profile.GetSetting<Vignette>();
        DoF = postProcessVolume.profile.GetSetting<DepthOfField>();
        distort = postProcessVolume.profile.GetSetting<LensDistortion>();
    }

    private void Update()
    {
        if(playerSanity.anxiety <= 15)
        {
            VignetteValueChanged(0.6f);
            DoFValueChanged(75);
            DistortValueChanged(-45);
        }
        else if (playerSanity.anxiety <= 40)
        {
            VignetteValueChanged(0.5f);
            DoFValueChanged(65);
            DistortValueChanged(-35);
        }
        else if (playerSanity.anxiety <= 60)
        {
            VignetteValueChanged(0.4f);
            DoFValueChanged(55);
            DistortValueChanged(-25);
        }
        else if (playerSanity.anxiety <= 85)
        {
            VignetteValueChanged(0.3f);
            DoFValueChanged(50);
            DistortValueChanged(-15);
        }
        else
        {
            VignetteValueChanged(0f);
            DoFValueChanged(40);
            DistortValueChanged(0f);
        }
        if(playerSanity.anxiety <= 85)
        {
            heartbeatCooldown = heartbeatCooldown - Time.deltaTime;
            if (heartbeatCooldown <= 0)
            {
                heartBeat.TriggerHeartbeat();
                heartbeatCooldown = 1;
                if (playerSanity.anxiety <= 20)
                {
                    heartBeat.heartbeatDuration = 0.2f;
                }
                else if (playerSanity.anxiety <= 40)
                {
                    heartBeat.heartbeatDuration = 0.4f;
                }
                else if (playerSanity.anxiety <= 60)
                {
                    heartBeat.heartbeatDuration = 0.6f;
                }
                else if (playerSanity.anxiety <= 85)
                {
                    heartBeat.heartbeatDuration = 0.8f;
                }
                else
                {
                    heartBeat.heartbeatDuration = 1f;
                }
            }
        }
    }

    public void VignetteValueChanged(float value)
    {
        // Update the vignette intensity based on the slider value
        if (vignette != null)
        {
            vignette.intensity.Override(value);
        }
    }

    public void DoFValueChanged(float value)
    {
        // Update the vignette intensity based on the slider value
        if (DoF != null)
        {
            if(SceneManager.GetActiveScene().name == "TrainGame")
            {
                DoF.focalLength.Override(value);
            }
        }
    }

    public void DistortValueChanged(float value)
    {
        // Update the vignette intensity based on the slider value
        if (distort != null)
        {
            distort.intensity.Override(value);
        }
    }
}
