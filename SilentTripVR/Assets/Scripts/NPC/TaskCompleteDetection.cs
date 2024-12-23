using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LLMUnity;
using UnityEngine.UI;

namespace LLMUnitySamples
{
    public class TaskCompleteDetection : MonoBehaviour
    {
        public LLMCharacter llmCharacter;
        public InputField playerText;
        private TaskHandler taskHandler;
        private string AIText;

        void Start()
        {
            taskHandler = GameObject.Find("TaskHandler").GetComponent<TaskHandler>();
            playerText.onSubmit.AddListener(onInputFieldSubmit);
            playerText.Select();
        }

        public void onInputFieldSubmit(string message)
        {
            playerText.interactable = false;
            _ = llmCharacter.Chat(message, SetAIText, AIReplyComplete);
        }

        public void SetAIText(string text)
        {
            AIText = text;
        }

        public void AIReplyComplete()
        {
            Debug.Log(AIText);
            taskHandler.CheckAnswer(AIText, taskHandler.stareDetection.gameObject.GetComponent<TaskList>());
            playerText.interactable = true;
            playerText.Select();
            playerText.text = "";
        }

        public void CancelRequests()
        {
            llmCharacter.CancelRequests();
            AIReplyComplete();
        }

        public void ExitGame()
        {
            Debug.Log("Exit button clicked");
            Application.Quit();
        }

        bool onValidateWarning = true;
        void OnValidate()
        {
            if (onValidateWarning && !llmCharacter.remote && llmCharacter.llm != null && llmCharacter.llm.model == "")
            {
                Debug.LogWarning($"Please select a model in the {llmCharacter.llm.gameObject.name} GameObject!");
                onValidateWarning = false;
            }
        }
    }
}
