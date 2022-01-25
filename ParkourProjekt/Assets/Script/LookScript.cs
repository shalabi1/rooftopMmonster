using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookScript : MonoBehaviour
{

    [SerializeField] private GameObject PlayerCam;

    private float LookX;
    private float LookY;

    private float xRotation;
    private float yRotation;

    public static float MouseSens = 8;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //sets the look values
        LookX = Input.GetAxisRaw("Mouse X");
        LookY = Input.GetAxisRaw("Mouse Y");


        xRotation += LookX * MouseSens;
        yRotation -= LookY * MouseSens;

        yRotation = Mathf.Clamp(yRotation, -90, 90);

        //Sets the transform rotation on playerbody
        this.transform.eulerAngles = new Vector3(0, xRotation, 0);
        //Sets the transform rotation for camera
        PlayerCam.transform.localEulerAngles = new Vector3(yRotation, 0, 0);

        //print (LookX);
    }
}
