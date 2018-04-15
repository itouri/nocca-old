using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
    public GameObject camera;

    private Vector3 defaultPosition;
    private Quaternion defaultAngle;
    private Vector3 lastMousePosition;
    private Vector3 newAngle = new Vector3(0, 0, 0);

    // Use this for initialization
    void Start () {
        defaultPosition = camera.transform.position;
        defaultAngle = camera.transform.rotation;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // マウスクリック開始(マウスダウン)時にカメラの角度を保持(Z軸には回転させないため).
            newAngle = camera.transform.localEulerAngles;
            lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            // マウスの移動量分カメラを回転させる.
            newAngle.y -= (Input.mousePosition.x - lastMousePosition.x) * 0.1f;
            newAngle.z -= (Input.mousePosition.y - lastMousePosition.y) * -0.1f;
            camera.transform.localEulerAngles = newAngle;

            lastMousePosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(1))
        {
            CameraReset();
        }
    }

    public void CameraReset()
    {
        //iTween.MoveUpdate(camera, iTween.Hash(
        //    "position", defaultPosition,
        //    "rotation", defaultAngle,
        //    "time", 1.0f));
        camera.transform.position = defaultPosition;
        camera.transform.rotation = defaultAngle;
    }
}
