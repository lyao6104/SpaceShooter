using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
	public float throttle, throttleSensitivity = 0.005f;
	public float maxSpeed = 10;
	public Vector3 steerStrength;
	public float steerSensitivity = 0.01f;

	private Rigidbody rb;
	private float dThrottle = 0;
	private Vector3 steering, dRotationEuler;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		Vector3 targetSteering = new Vector3(steerStrength.x * steering.x, steerStrength.y * steering.y, steerStrength.z * steering.z);
		dRotationEuler = Vector3.MoveTowards(dRotationEuler, targetSteering, steerSensitivity);
		rb.AddForce(transform.forward * maxSpeed * throttle);
		rb.MoveRotation(rb.rotation * Quaternion.Euler(dRotationEuler));
	}

	private void Update()
	{
		throttle = Mathf.Clamp(throttle + dThrottle, -1, 1);
	}

	public void OnThrottle(InputValue value)
	{
		dThrottle = value.Get<float>() * throttleSensitivity;
	}

	public void OnKillThrottle()
	{
		throttle = 0;
	}

	public void OnPitch(InputValue value)
	{
		steering.x = value.Get<float>();
	}

	public void OnYaw(InputValue value)
	{
		steering.y = value.Get<float>();
	}

	public void OnRoll(InputValue value)
	{
		steering.z = value.Get<float>();
	}
}
