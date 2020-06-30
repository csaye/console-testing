using System;
using TMPro;
using UnityEngine;

namespace ConsoleTesting
{
    public class Console : MonoBehaviour
    {
        [Header("References")]
        public GameObject messagePrefab;
        public CanvasGroup canvasGroup;
        public TMP_InputField inputField;
        public RectTransform content;

        private float messageCount;
        private float maxMessages = 30;

        private bool consoleActive;

        private void Start()
        {
            UpdateConsoleActive();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Slash))
            {
                if (!consoleActive)
                {
                    consoleActive = true;
                    UpdateConsoleActive();
                    inputField.text = "/";
                }
            }

            if (Input.GetKeyDown("escape"))
            {
                consoleActive = !consoleActive;
                UpdateConsoleActive();
            }

            if (Input.GetKeyDown("return"))
            {
                if (consoleActive)
                {
                    RunCommand(inputField.text);
                    consoleActive = false;
                    UpdateConsoleActive();
                }
            }
        }

        private void UpdateConsoleActive()
        {
            canvasGroup.alpha = consoleActive ? 1 : 0;
            canvasGroup.interactable = consoleActive;
            canvasGroup.blocksRaycasts = consoleActive;

            if (consoleActive)
            {
                inputField.ActivateInputField();
            }
            else
            {
                inputField.text = "";
            }
        }

        private void RunCommand(string command)
        {
            if (!command.StartsWith("/"))
            {
                Output(command);
                return;
            }

            string primaryPhrase;

            if (!command.Contains(" "))
            {
                primaryPhrase = command.Substring(1);
            }
            else
            {
                primaryPhrase = command.Substring(1, command.IndexOf(" ") - 1);
            }

            switch (primaryPhrase)
            {
                case "say":
                    if (command.Contains(" ")) Output(command.Substring(command.IndexOf(" ")));
                    break;
                case "shout":
                    if (command.Contains(" ")) Output(command.Substring(command.IndexOf(" ")).ToUpper());
                    break;
                case "double":
                    if (command.Contains(" ")) try { Output((2 * Int32.Parse(command.Substring(command.IndexOf(" ")))).ToString()); } catch { Output(command.Substring(command.IndexOf(" ")) + " is an invalid number."); }
                    break;
                default:
                    Output("Command " + primaryPhrase + " not found.");
                    break;
            }
        }

        private void Output(string message)
        {
            messageCount++;

            // If number of messages exceeds max count, destroy oldest message
            if (messageCount > maxMessages)
            {
                Destroy(content.GetChild(0).gameObject);
            }

            GameObject output = Instantiate(messagePrefab, content);
            output.GetComponent<TextMeshProUGUI>().text = message;
        }
    }
}
