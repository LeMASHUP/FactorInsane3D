using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchSelection : MonoBehaviour
{
    RawImage image;
    private void Start()
    {
        image = GetComponent<RawImage>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Vector3 pos = new Vector3(1338, image.transform.position.y, image.transform.position.z);
            image.rectTransform.position = pos;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Vector3 pos = new Vector3(1470, image.transform.position.y, image.transform.position.z);
            image.rectTransform.position = pos;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Vector3 pos = new Vector3(1600, image.transform.position.y, image.transform.position.z);
            image.rectTransform.position = pos;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Vector3 pos = new Vector3(1725, image.transform.position.y, image.transform.position.z);
            image.rectTransform.position = pos;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Vector3 pos = new Vector3(1855, image.transform.position.y, image.transform.position.z);
            image.rectTransform.position = pos;
        }
    }
}
