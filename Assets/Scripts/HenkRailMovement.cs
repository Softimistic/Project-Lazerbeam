using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HenkRailMovement: MonoBehaviour
{
    private Transform _transform; //Tranform attached to Henk
    private Vector3 _virtualRotation;
    private Rigidbody _henkBody;
    private bool[] _allowRotationReset; //Wether or not resetting the rotation is allowed ([0] is for X axis, [1] for Y and Z)
    public float Speed; //Speed at which Henk moves horizontally and vertically
    public float ForwardSpeed; //Speed at which Henk automatically moves forward

    void Start()
    {
        _henkBody = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
        _virtualRotation = new Vector3(0, 0, 0);
    }

    void Update()
    {
        //print("X: " + _transform.rotation.eulerAngles.x + ", Y:" + _transform.rotation.eulerAngles.y + ", Z:" + _transform.rotation.eulerAngles.z);

        _allowRotationReset = new bool[] { true, true };
        Move();
        ResetTilt();
        //AddVirtualRotationToTransform();
    }

    /// <summary>
    /// Moves Henk, and applies rotation
    /// </summary>
    private void Move()
    {
        if (Input.GetAxis("Horizontal") != 0) //Dont't reset the Y and Z rotation if is moving (and thus rotating Y and Z) horizontally
        {
            _allowRotationReset[1] = false;
        }
        if (Input.GetAxis("Vertical") != 0) //Dont't reset the X rotation if is moving (and thus rotating X) vertically
        {
            _allowRotationReset[0] = false;
        }

        /*_transform.SetPositionAndRotation(new Vector3( //Move Henk, and apply rotation fitting that movement
            _transform.position.x + Input.GetAxis("Horizontal") * Speed,
            _transform.position.y + Input.GetAxis("Vertical") * Speed,
            _transform.position.z + ForwardSpeed),
            _transform.rotation);*/

        _henkBody.AddForce(new Vector3(
            Input.GetAxis("Horizontal") * Speed,
            Input.GetAxis("Vertical") * Speed, 
            ForwardSpeed));

        print(_henkBody.velocity.x + ", " + _henkBody.velocity.y + ", " + _henkBody.velocity.z);

        int rotationMin = -45;
        int rotationMax = 45;

        _virtualRotation = new Vector3( //Change rotation but limit it to 45 degreess
            LimitRotation(_virtualRotation.x - Input.GetAxis("Vertical") * Speed, rotationMin, rotationMax),
            LimitRotation(_virtualRotation.y + Input.GetAxis("Horizontal") * Speed, rotationMin, rotationMax),
            LimitRotation(_virtualRotation.z + Input.GetAxis("Horizontal") * Speed, rotationMin, rotationMax));
    }

    /// <summary>
    /// Decreases the rotation (if allowed)
    /// </summary>
    private void ResetTilt()
    {
        if (_allowRotationReset[0]) //Reset X rotation
        {
            _virtualRotation = new Vector3(
            CalcAxisReset(_virtualRotation.x, Speed),
            _virtualRotation.y,
            _virtualRotation.z);
        }
        if (_allowRotationReset[1]) //Reset Y and Z rotation
        {
            _virtualRotation = new Vector3(
            _virtualRotation.x,
            CalcAxisReset(_virtualRotation.y, Speed),
            CalcAxisReset(_virtualRotation.z, Speed));
        }
    }

    /// <summary>
    /// Returns a rotation that is within the desired range
    /// </summary>
    /// <param name="axis"></param> Axis that will be limited
    /// <param name="min"></param> Minimum rotation
    /// <param name="max"></param> Maximum rotation
    /// <returns></returns> Rotation adjusted to be withing the desired range
    private float LimitRotation(float axis, float min, float max)
    {
        axis = AdjustAxis(axis);
        if (axis < min)
        {
            return min;
        }
        else if (axis > max)
        {
            return max;
        }
        else
        {
            return axis;
        }
    }

    /// <summary>
    /// Change the rotation to make it closer to 0
    /// </summary>
    /// <param name="axis"></param> Axis that will be modified
    /// <param name="amount"></param> Amount that will be added/removed
    /// <returns></returns> Modified rotation
    private float CalcAxisReset(float axis, float amount)
    {
        axis = AdjustAxis(axis);

        if (Mathf.RoundToInt(axis) == 0)
        {
            return 0;
        }
        else if (axis > 0)
        {
            return axis - amount;
        }
        else
        {
            return axis + amount;
        }
    }

    /// <summary>
    /// Adjust the rotation to make it appear as it does in the editor
    /// </summary>
    /// <param name="axis"></param> Rotation that will be adjusted
    /// <returns></returns> Adjusted rotation
    private float AdjustAxis(float axis)
    {
        if (axis > 180)
        {
            return axis - 360;
        }
        else
        {
            return axis;
        }
    }

    //Clamp player to camera
    private void ClampPlayer()
    {
        /*Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        pos.z = Mathf.Clamp(pos.z, 2f, 10f);
        transform.position = Camera.main.ViewportToWorldPoint(pos);*/
    }

    private void AddVirtualRotationToTransform()
    {
        _transform.eulerAngles = new Vector3(
            _transform.eulerAngles.x + _virtualRotation.x,
            _transform.eulerAngles.y + _virtualRotation.y,
            _transform.eulerAngles.z + _virtualRotation.z);
    }
}
