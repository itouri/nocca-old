using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
    public GameObject camera;
    public float verticalSensitivity;
    public float horizontalSensitivity;

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
            //TODO このコードの理解
            // マウスの移動量
            float mouseInputX = Input.GetAxis("Mouse X");
            float mouseInputY = Input.GetAxis("Mouse Y");
            // targetの位置のY軸を中心に、回転（公転）する
            transform.RotateAround(Vector3.zero, Vector3.up, mouseInputX * Time.deltaTime * horizontalSensitivity);
            // カメラの垂直移動（※角度制限なし、必要が無ければコメントアウト）
            transform.RotateAround(Vector3.zero, transform.right, mouseInputY * Time.deltaTime * verticalSensitivity);
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
