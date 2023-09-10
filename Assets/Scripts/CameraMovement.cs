using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float cameraSpeed;
    private Transform mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraMove = mainCamera.position;
        cameraMove.x += cameraSpeed * Time.deltaTime;
        mainCamera.position = cameraMove;
    }
}
