using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public GameObject machineUI;
    public GameObject normalUI;

    public void Quit()
    {
        normalUI.SetActive(true);
        machineUI.SetActive(false);
    }
}
