using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonManager : MonoBehaviour
{
    public GameObject machineUI;
    public GameObject normalUI;
    public GameObject machineObject;
    public TMP_InputField inputFieldBlocks;
    public TMP_InputField inputFieldRounds;
    public string inputBlocks;
    public int outputBlocks;
    public string inputRounds;
    public int outputRounds;

    public void Quit()
    {
        normalUI.SetActive(true);
        machineUI.SetActive(false);
    }

    public void QuitDone()
    {
        normalUI.SetActive(true);
        machineUI.SetActive(false);
        if (machineObject.name == "BlocksMachine(Clone)")
        {
            inputBlocks = inputFieldBlocks.text.ToString();
            outputBlocks = int.Parse(inputBlocks);
            machineObject.GetComponent<CreateBlocks>().maxBlocks = outputBlocks;

            inputRounds = inputFieldRounds.text.ToString();
            outputRounds = int.Parse(inputRounds);
            machineObject.GetComponent<CreateBlocks>().interval = machineObject.GetComponent<CreateBlocks>().interval * outputRounds;
        }
    }
}
