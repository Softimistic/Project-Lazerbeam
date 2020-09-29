using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HenkRailMovement: MonoBehaviour
{
    private Transform _transform; //Tranform attached to Henk
    private bool[] _allowRotationReset; //Wether or not resetting the rotation is allowed ([0] is for X axis, [1] for Y and Z)
    public float Speed; //Speed at which Henk moves horizontally and vertically
    public float ForwardSpeed; //Speed at which Henk automatically moves forward

    void Start()
    {
        _transform = GetComponent<Transform>();
    }

    void Update()
    {
        print("X: " + _transform.rotation.eulerAngles.x + ", Y:" + _transform.rotation.eulerAngles.y + ", Z:" + _transform.rotation.eulerAngles.z);

        _allowRotationReset = new bool[] { true, true };
        Move();
        ResetTilt();
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

        _transform.SetPositionAndRotation(new Vector3( //Move Henk, and apply rotation fitting that movement
            _transform.position.x + Input.GetAxis("Horizontal") * Speed,
            _transform.position.y + Input.GetAxis("Vertical") * Speed,
            _transform.position.z + ForwardSpeed),
            Quaternion.Euler(new Vector3(
                _transform.rotation.eulerAngles.x - Input.GetAxis("Vertical") * Speed,
                _transform.rotation.eulerAngles.y + Input.GetAxis("Horizontal") * Speed,
                _transform.rotation.eulerAngles.z - Input.GetAxis("Horizontal") * Speed)));

        int rotationMin = -45;
        int rotationMax = 45;
        Vector3 rotation = _transform.rotation.eulerAngles;

        _transform.eulerAngles = new Vector3( //Limit rotation
            LimitRotation(rotation.x, rotationMin, rotationMax),
            LimitRotation(rotation.y, rotationMin, rotationMax),
            LimitRotation(rotation.z, rotationMin, rotationMax));
    }

    /// <summary>
    /// Decreases the rotation (if allowed)
    /// </summary>
    private void ResetTilt()
    {
        Vector3 rotation = _transform.rotation.eulerAngles;

        if (_allowRotationReset[0]) //Reset X rotation
        {
            _transform.eulerAngles = new Vector3(
            CalcAxisReset(rotation.x, Speed),
            _transform.rotation.eulerAngles.y,
            _transform.rotation.eulerAngles.z);
        }
        if (_allowRotationReset[1]) //Reset Y and Z rotation
        {
            _transform.eulerAngles = new Vector3(
            _transform.rotation.eulerAngles.x,
            CalcAxisReset(rotation.y, Speed),
            CalcAxisReset(rotation.z, Speed));
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
    void ClampPlayer()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        pos.z = Mathf.Clamp(pos.z, 2f, 10f);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }
}
