using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Camera mainCamera;
    // Start is called before the first frame update
    private void Start()
    {
        mainCamera = Camera.main;// switched to this cuz i got too lazy to add the camera to the script everytime
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.back, mainCamera.transform.rotation * Vector3.up);
        this.transform.Rotate(0, 180, 0);
    }
}
