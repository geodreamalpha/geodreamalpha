using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public GameObject subject;
    public Vector3 viewDistance = new Vector3(0, 2, 0);
    public float sensitivity = 3f;
    Vector3 rotation;
    Vector3 lerped;

    // Start is called before the first frame update
    void Start()
    {
        viewDistance = new Vector3(0, 3, -5);
        sensitivity = 2f;
        transform.position = subject.transform.position + viewDistance;
    }//

    // Update is called once per frame
    void Update()
    {
        if (!TerrainGeneratorComponent.TerrainGenerator.isPaused)
        {
            Vector3 cameraPosition = transform.position;
            cameraPosition.y = subject.transform.position.y;
            transform.position = cameraPosition;

            float rotateHorizontal = Input.GetAxis("Mouse Y");
            float rotateVertical = Input.GetAxis("Mouse X");

            //rotation.x += -rotateHorizontal * sensitivity;
            //rotation.y += rotateVertical * sensitivity;

            //rotation.x = Mathf.Repeat(rotation.x, 360);
            //rotation.y = Mathf.Repeat(rotation.y, 360);

            transform.RotateAround(subject.transform.position, Vector3.up, rotateVertical * sensitivity);
            //transform.Rotate(Vector3.up, rotateVertical * sensitivity);
            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(rotation), 10f * Time.deltaTime);
        }   
    }//

    public void Oscillate()
    {

    }//
}
