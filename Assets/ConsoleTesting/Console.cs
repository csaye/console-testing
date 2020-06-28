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
                    inputField.text = "";
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
        }

        private void RunCommand(string command)
        {
        
        }
    }
}
