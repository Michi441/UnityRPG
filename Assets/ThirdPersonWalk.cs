using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ThirdPersonWalk : MonoBehaviour {


	[SerializeField] float t_MovingTurnSpeed = 360;
	[SerializeField] float t_StationaryTurnSpeed = 180;
	[SerializeField] float t_JumpPower = 12f;
	[Range(1f, 4f)][SerializeField] float t_GravityMultiplier = 2f;
	[SerializeField] float t_MoveSpeedMultiplier = 1f;

	Rigidbody t_RigidBody;
	Animator anim;
	float turnAmount;
	float forwardAmount;
	Vector3 t_GroundNormal;
	float t_CapsuleHeight;
	Vector3 t_CapsuleCenter;
	CapsuleCollider t_Capsule;
	bool t_Crouching;

	// Use this for initialization
	void Start () {

		anim = GetComponent<Animator> ();
		t_RigidBody = GetComponent<Rigidbody> ();
		t_Capsule = GetComponent<CapsuleCollider> ();
		t_CapsuleHeight = t_Capsule.height;
		t_CapsuleCenter = t_Capsule.center;

		t_RigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

	}


	public void Movez(Vector3 move){
		
		if (move.magnitude > 1f)
			move.Normalize ();
		move = transform.InverseTransformDirection (move);
		move = Vector3.ProjectOnPlane (move, t_GroundNormal);
		turnAmount = Mathf.Atan2 (move.x, move.z);
		forwardAmount = move.z;


		UpdateAnimator (move);

		ApplyExtraTurnRotation ();


	}

	void ApplyExtraTurnRotation()
	{
		// help the character turn faster (this is in addition to root rotation in the animation)
		float turnSpeed = Mathf.Lerp(t_StationaryTurnSpeed, t_MovingTurnSpeed, forwardAmount);
		transform.Rotate(0, turnAmount * turnSpeed * Time.deltaTime, 0);
	}


	public void OnAnimatorMove(){


		Vector3 v = (anim.deltaPosition * t_MoveSpeedMultiplier) / Time.deltaTime;

		v.y = t_RigidBody.velocity.y;
		t_RigidBody.velocity = v;

	}
	
	// Update is called once per frame
	void FixedUpdate () {



		
	}

	void UpdateAnimator(Vector3 move){



		anim.SetFloat ("Forward", forwardAmount, 0.1f, Time.deltaTime);
		anim.SetFloat ("Turn", turnAmount, 0.1f, Time.deltaTime);

	}
}
