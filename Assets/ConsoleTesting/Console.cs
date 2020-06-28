using System;
using TMPro;
using UnityEngine;

namespace ConsoleTesting
{
    public class Console : MonoBehaviour
    {
        [Header("References")]
        public CanvasGroup canvasGroup;
        public TMP_InputField inputField;

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
                Debug.LogError("Command " + command + " does not start with slash.");
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
                    if (command.Contains(" ")) Debug.Log(command.Substring(command.IndexOf(" ")));
                    break;
                case "shout":
                    if (command.Contains(" ")) Debug.Log(command.Substring(command.IndexOf(" ")).ToUpper());
                    break;
                case "double":
                    if (command.Contains(" ")) try { Debug.Log(2 * Int32.Parse(command.Substring(command.IndexOf(" ")))); } catch { Debug.LogError(command.Substring(command.IndexOf(" ")) + " is an invalid number."); }
                    break;
                default:
                    Debug.LogError("Command " + primaryPhrase + " not found.");
                    break;
            }
        }
    }
}
