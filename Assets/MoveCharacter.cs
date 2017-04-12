using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.ThirdPerson
{


	[RequireComponent(typeof (ThirdPersonWalk))]


public class MoveCharacter : MonoBehaviour {


		private ThirdPersonWalk m_Character;
		private Transform m_Cam;
		private Vector3 m_CamForward;
		private Vector3 m_Move;
		private bool m_Jump;

	// Use this for initialization
	void Start () {


			if (Camera.main != null) {

				m_Cam = Camera.main.transform;
			} else {

				Debug.LogWarning ("No Main Camera found");

			}

			m_Character = GetComponent<ThirdPersonWalk> ();
		
	}
	
	// Update is called once per frame
	private void FixedUpdate () {


			float h = Input.GetAxis ("Horizontal");
			float v = Input.GetAxis ("Vertical");


			Debug.Log(Input.GetAxis("Horizontal"));

			if (m_Cam != null)
			{
				// calculate camera relative direction to move:
				m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
				m_Move = v*m_CamForward + h*m_Cam.right;
			}
			else
			{
				// we use world-relative directions in the case of no main camera
				m_Move = v*Vector3.forward + h*Vector3.right;
			}



			m_Character.Movez(m_Move);

				Debug.Log(m_Character.transform);

		
	}

	
}


}
