using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AgentController : MonoBehaviour
{
    public float EffectorRange = 1.0f;
    public float maxHeadTurn = 45.0f;
    public float turnSpeed = 0.1f;
    public float fieldOfView = 10f;
    public GameObject tracked = null;
    bool leftTurning = true;
    Transform face;
    Transform leftEye, rightEye;
    Vector3 leftCenter, rightCenter;

    // Start is called before the first frame update
    void Start()
    {
        face = transform.Find("Face");
        leftEye = face.transform.Find("Left Eye").transform;
        rightEye = face.transform.Find("Right Eye").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        var gamepad = Gamepad.current;
        if (gamepad == null){
            Debug.Log("No Controller Connected");
            return; // No gamepad connected.
        }
        
        if (leftTurning){
            //face.Rotate(0,-turnSpeed,0);
        }
        else{
            //face.Rotate(0,turnSpeed,0);
        }

        if (Mathf.Abs(face.rotation.y) * 360 > maxHeadTurn){
            leftTurning ^= true;
        }
        leftCenter = leftEye.position;
        rightCenter = rightEye.position;

        Vector2 leftMove = gamepad.leftStick.ReadValue() * EffectorRange;
        Vector2 rightMove = gamepad.rightStick.ReadValue() * EffectorRange;
        Vector3 newLeft = new Vector3(leftMove.x + leftCenter.x, leftMove.y+leftCenter.y, leftCenter.z);
        Vector3 newRight = new Vector3(rightMove.x + rightCenter.x, rightMove.y+rightCenter.y, rightCenter.z);

/*
        leftEye.rotation = Quaternion.FromToRotation(leftEye.position, newLeft);
        rightEye.rotation = Quaternion.FromToRotation(rightEye.position, newRight);
        rightEye.RotateAround(rightEye.position, Vector3.up, rightMove.x);
        rightEye.RotateAround(rightEye.position, Vector3.right, -rightMove.y);
*/

        //rightEye.rotation = Quaternion.Euler(0, 0, 0);
        //rightEye.rotation *= Quaternion.Euler(-rightMove.y, rightMove.x, 0);
        //rightEye.rotation = Quaternion.Euler(0, 0, 0);
        //rightEye.position = Vector3.RotateTowards(rightEye.forward, face.forward,4,1);

        //rightEye.position = newRight;
        rightEye.localEulerAngles = new Vector3(-rightMove.y, rightMove.x + fieldOfView/2f,0);
        leftEye.localEulerAngles = new Vector3(-leftMove.y, leftMove.x - fieldOfView/2,0);

        if (tracked != null){
            Vector3 diff = tracked.transform.position - rightEye.position;
            float angle = Vector3.Angle(diff, rightEye.forward);
            Debug.Log(1/angle);
        }
        else{
            Debug.Log("No object to track");
        }
    }
}
