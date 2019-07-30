using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseAndInstructions : MonoBehaviour
{

    [SerializeField] private GameObject instructionsPanel;
    // Update is called once per frame

    bool inInstructions;

    void Update()
    {
        if (Input.GetButtonDown(ActionsTags.INSTRUCTIONS))
        {
            if (!inInstructions)
            {
                instructionsPanel.SetActive(true);
                inInstructions = true;
            } else
            {
                instructionsPanel.SetActive(false);
                inInstructions = false;
            }
        }

        if (inInstructions && Input.GetButtonDown(ActionsTags.QUIT))
        {
            Application.Quit();
        }
    }
}
