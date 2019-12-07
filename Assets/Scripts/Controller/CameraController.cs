using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;

    private float cam_height = -2f;
    private Vector3 offset;

    private float current_zoom = 10f;
    private float zoom_speed = 4f;
    public float pitch = 2f;
    private float current_yaw = 0f, yaw_speed = 1600f;

    void Start()
    {
        this.offset = new Vector3(-1f,this.cam_height,-1f);
    }

    void Update()
    {
        current_zoom -= Input.GetAxis("Mouse ScrollWheel") * this.zoom_speed;
        current_zoom = Mathf.Clamp( current_zoom, 5f, 15f);
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)) 
        {
            float signo = Input.GetKey(KeyCode.W) ? -1f : 1f;
            this.offset.y += 0.03f * signo;
            this.offset.y = Mathf.Clamp(this.offset.y, -2f, - 0.20f );
        }

        current_yaw -= Input.GetAxis("Horizontal") * yaw_speed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            current_yaw = -19.62f;
            current_zoom = 15f;
            this.offset.y = -2f;

        }
    }

    void LateUpdate()
    {
        if(target != null)
        {
            transform.position = target.position - offset * current_zoom;
            transform.LookAt(target.position + Vector3.up * pitch);

            transform.RotateAround(target.position, Vector3.up, current_yaw);
        }
    }
}
