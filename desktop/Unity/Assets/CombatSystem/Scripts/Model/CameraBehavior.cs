using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour {

    public Transform subject;
    [SerializeField]
    Vector3 cameraOffset = new Vector3(0, 5, -5);
    [SerializeField]
    public float smoothFactor = 0.1f;
    [SerializeField]
    public float rotationsSpeed = 3.0f;

    void LateUpdate()
    {
        #region Set cameraOffset
        //camera offset represents the view distance the camera is from the subject (player).
        //this can be adjusted using the Input.mouseScrollDelta.y (scroll wheel)
        cameraOffset += transform.TransformDirection(new Vector3(0f, 0f, Input.mouseScrollDelta.y * 0.5f));
        #endregion

        #region Set Camera's Horizontal Orientation Around Player by Getting Mouse Input For X Axis
        //stores the camera's horizontal rotation around the player.  Determined by mouse movement along the screen.
        Quaternion offsetHorizontal =
            Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationsSpeed, Vector3.up);
        #endregion

        #region Set Camera's Vertical Orientation Around Player by Getting Mouse Input For Y Axis
        //stores the camera's vertical rotation around the player.  Determined by mouse movement along the screen.
        Quaternion offsetVertical =
            Quaternion.AngleAxis(-Input.GetAxis("Mouse Y") * rotationsSpeed / 2, transform.right);
        #endregion

        #region Add Mouse Input Orientation To _cameraOffset
        //adds the orientations calculated above to _cameraOffset (the distance between camera and player)
        cameraOffset = offsetHorizontal * offsetVertical * cameraOffset;
        #endregion

        #region Update Camera Position With the Newly Calculated _cameraOffset
        //the Lerp() function is used to smoothly transform camera from previous position to new position (it happens over multiple frames)
        //the smoothFactor controls the speed of this transition between the range of [0, 1].
        //the newly calculated _cameraOffset AND player's position are added together to create camera's new position
        transform.position = Vector3.Lerp(transform.position, subject.position + cameraOffset, smoothFactor * 60f * Time.deltaTime);
        #endregion

        #region Update Camera Rotation Toward Player's Position
        //previously, we have moved the camera's position around the player using input from the mouse.
        //this method moves the camera's rotation to face the player
        transform.LookAt(subject);
        #endregion
    }
}
