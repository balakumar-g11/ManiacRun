/**
 * File : CameraControl.cs
 * Author : Balakumar Marimuthu
 * Use : The script makes the camera to follow the player at any point of time
 **/

using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{
    public Transform target; //Target to follow
    public float angle = 15; //Angle camera
    public float distance = -10; //Distance target
    public float height = 10.0f; // Height camera


    //Private variable field
    private Vector3 posCamera;
    private Vector3 angleCam;
    private bool shake;
    public static CameraControl instance;

    void Start()
    {
        instance = this;
        transform.position = new Vector3(0.0f, 6.0f, -6.0f);
    }

    void LateUpdate()
    {
        if (target != null)
        {
            if (target.position.z >= 0)
            {
                if (shake == false)
                {
                    posCamera.y = Mathf.Lerp(posCamera.y, target.position.y + height, 5 * Time.deltaTime);
                    posCamera.z = Mathf.Lerp(posCamera.z, target.position.z + distance, 5 * Time.deltaTime);
                    transform.position = posCamera;
                }
            }
        }
    }


    //Reset camera when charater die
    public void Reset()
    {
        shake = false;
        Vector3 dummy = Vector3.zero;
        posCamera.x = 0;
        posCamera.y = dummy.y + height;
        posCamera.z = dummy.z + distance;
        transform.position = posCamera;
    }

    //Shake camera
    public void ActiveShake()
    {
        shake = true;
        StartCoroutine(ShakeCamera());
    }

    IEnumerator ShakeCamera()
    {
        float count = 0;
        Vector3 pos = Vector3.zero; ;
        while (count <= 0.2f)
        {
            count += 1 * Time.smoothDeltaTime;
            pos.x = transform.position.x + Random.Range(-0.05f, 0.05f);
            pos.y = target.position.y + height;
            pos.z = target.position.z + distance;
            transform.position = pos;
            yield return 0;
        }
        transform.position = posCamera;
        posCamera.x = transform.position.x;
        posCamera.y = target.position.y + height;
        posCamera.z = target.position.z + distance;
        transform.position = posCamera;
        shake = false;
    }

}
