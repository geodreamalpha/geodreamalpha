using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviorHelper : MonoBehaviour
{
    Camera mainCamera;
    CharacterController controller;
    Vector3 gravity;
    float speed;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        controller = this.GetComponent<CharacterController>();
        speed = 5;
        gravity = new Vector3(0, -9.8f, 0);
    }//

    // Update is called once per frame
    void Update()
    {
        if (!TerrainGeneratorComponent.TerrainGenerator.isPaused)
        {
            if (Input.GetKey(KeyCode.W))
            {
                controller.Move(mainCamera.transform.forward * Time.deltaTime * speed);
                mainCamera.GetComponent<CameraBehavior>().Oscillate();
            }
            if (Input.GetKey(KeyCode.S))
            {
                controller.Move(-mainCamera.transform.forward * Time.deltaTime * speed);
                mainCamera.GetComponent<CameraBehavior>().Oscillate();
            }

            //add gravity
            controller.Move(gravity * Time.deltaTime);
        } 
    }
}
