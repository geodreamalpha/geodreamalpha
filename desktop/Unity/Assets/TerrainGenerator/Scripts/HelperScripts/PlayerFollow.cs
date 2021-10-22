using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour {

    public Transform PlayerTransform;

    [SerializeField]
    private Vector3 _cameraOffset;

    [Range(0.001f, 1.0f)]
    public float SmoothFactor = 0.1f;

    public bool LookAtPlayer = false;

    public bool RotateAroundPlayer = true;

    public float RotationsSpeed = 3.0f;

	void Start () 
    {
        //_cameraOffset = transform.position - PlayerTransform.position;
        _cameraOffset = new Vector3(0, 5, -5);
	}
	
	void LateUpdate () 
    {
        Vector3 playerPosition = PlayerTransform.position;

        _cameraOffset += transform.TransformDirection(new Vector3(0f, 0f, Input.mouseScrollDelta.y * 0.5f));

        if(RotateAroundPlayer)
        {
            Quaternion offsetHorizontal =
                Quaternion.AngleAxis(Input.GetAxis("Mouse X") * RotationsSpeed, Vector3.up);

            Quaternion offsetVertical =
                Quaternion.AngleAxis(-Input.GetAxis("Mouse Y") * RotationsSpeed / 2, transform.right);

            _cameraOffset = offsetHorizontal * offsetVertical * _cameraOffset;
        }

        Vector3 newPos = playerPosition + _cameraOffset;

        transform.position = Vector3.Lerp(transform.position, newPos, SmoothFactor);

        if (LookAtPlayer || RotateAroundPlayer)
        {
            transform.LookAt(playerPosition);
        }
            
	}
}
