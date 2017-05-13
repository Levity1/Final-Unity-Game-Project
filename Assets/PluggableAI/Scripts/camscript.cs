using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camscript : MonoBehaviour
{
    public float camspeed = 30;
    public float zoomspeed = 10;
    public static float ScrollWheel { get { return Input.GetAxis("Mouse ScrollWheel"); } }
    // Use this for initialization
    public float turnspeed = 60;
    public float minFov = 15f;
    public float maxFov = 90f;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float fov = Camera.main.fieldOfView;

        float y = Input.mouseScrollDelta.y;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        if (y > 0)
        {
            fov += -zoomspeed;
            Camera.main.fieldOfView = fov;

        }
        if (y < 0)
        {
            fov += zoomspeed;
            Camera.main.fieldOfView = fov;

        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, 0, camspeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, 0, -camspeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(new Vector3(-camspeed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(new Vector3(camspeed * Time.deltaTime, 0, 0));
        }
        /*     if (Input.GetKey(KeyCode.E))
             {

                 transform.Rotate(Vector3.up, turnspeed * Time.deltaTime,Space.World);

             }
             if (Input.GetKey(KeyCode.Q))
             {

                 transform.Rotate(Vector3.down, turnspeed * Time.deltaTime,Space.World);

             }*/
    }
}
