using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxAnim : MonoBehaviour
{
	private CharacterController characterController;
	private Vector3 velocity;
	[SerializeField]
	private float walkSpeed;
	private Animator animator;

	// Use this for initialization
	void Start()
	{
		characterController = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
	{
		//if (characterController.isGrounded)
		//{
			velocity = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

			if (velocity.magnitude > 0.1f)
			{
				animator.SetFloat("speed", velocity.magnitude);
				transform.LookAt(transform.position + velocity);
			}
			else
			{
				animator.SetFloat("speed", 0f);
			}
		//}
		velocity.y += Physics.gravity.y * Time.deltaTime;
	}
}
