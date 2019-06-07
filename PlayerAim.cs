using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PlayerAim : MonoBehaviour {

    public static PlayerAim singleton;

    public GameObject rotation_joint;
    public float rotation_joint_rotate_speed;

    void Awake()
    {
        singleton = this;
    }
	
	void Update () {
        RotatePlayerAim();
	}

    /// <summary>
    /// Converts joystick vector to rotation and updates the rotation of rotation_joint
    /// </summary>
    /// 

    void RotatePlayerAim()
    {
        Vector2 mouseDir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - rotation_joint.transform.position).normalized;

        float angle = QuickMaths.VectorToAngle(mouseDir.x, mouseDir.y);
        rotation_joint.transform.localEulerAngles = new Vector3(rotation_joint.transform.localEulerAngles.x,
                                                                rotation_joint.transform.localEulerAngles.y,
                                                                angle -90);
    }

    void UpdateIndicatorRotation()
    {
        // rotation_joint.transform.localEulerAngles = new Vector3(rotation_joint.transform.localEulerAngles.x, rotation_joint.transform.localEulerAngles.y
        //                                                   , JoystickLeftAngle() - 90);
    
        if (RotationInput() == -1)
        {
            RotateAimer(-rotation_joint_rotate_speed);
        }
        else if (RotationInput() == 1)
        {
            RotateAimer(rotation_joint_rotate_speed);
        }
    }

    int RotationInput()
    {
        if (InputManager.ActiveDevice.LeftStickX > 0.2 || Input.GetKey(KeyCode.D))
        {
            float newRot = rotation_joint.transform.localEulerAngles.z + (-rotation_joint_rotate_speed * Time.deltaTime);
            if ((newRot < 90 ) || (newRot > 270))
                return -1;
        }
        else if (InputManager.ActiveDevice.LeftStickX < -0.2 || Input.GetKey(KeyCode.A))
        {
            float newRot = rotation_joint.transform.localEulerAngles.z + (rotation_joint_rotate_speed * Time.deltaTime);             
            if ((newRot < 90 ) || (newRot > 270))
                return 1;
        }
        return 0;
    }

    void RotateAimer(float amount)
    {
        rotation_joint.transform.localEulerAngles = new Vector3(rotation_joint.transform.localEulerAngles.x, rotation_joint.transform.localEulerAngles.y,
                                                               rotation_joint.transform.localEulerAngles.z + (amount * Time.deltaTime)); 
    }

    float JoystickLeftAngle()
    {
        float y_value = InputManager.ActiveDevice.LeftStickY;
        if (y_value < 0)
        {
            y_value = 0;
        }

        return VectorToAngle(InputManager.ActiveDevice.LeftStickX, y_value);
    }

    public float VectorToAngle(float x, float y)
    {
        return Mathf.Atan2(y, x) * Mathf.Rad2Deg;
    }


    public Vector2 AngleToVector(float angle)
    {
        float newAngle = angle * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(newAngle), Mathf.Sin(newAngle));
    }

    public Vector2 DirectionVector()
    {   
        return AngleToVector(rotation_joint.transform.localEulerAngles.z + 90);
    }
}
