using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CharacterController))]
[RequireComponent (typeof (Animator))]
public class CharacterControl2D : MonoBehaviour {
	CharacterController controller;
	Animator animator;
	Transform transform;
	Camera mainCamera;

	public float walkingSpeed = 0.1f;

	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
		transform = GetComponent<Transform> ();
		mainCamera = Camera.main;
	}

	CharacterDirection determineCharacterDirection ()
	{
		if (Input.GetKey (KeyCode.RightArrow)) {
			if(Input.GetKey (KeyCode.UpArrow)) {
				return CharacterDirection.UPRIGHT;
			} 
			if (Input.GetKey (KeyCode.DownArrow)) {
				return CharacterDirection.DOWNRIGHT;
			}
			return CharacterDirection.RIGHT;
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			if(Input.GetKey (KeyCode.UpArrow)) {
				return CharacterDirection.UPLEFT;
			} 
			if (Input.GetKey (KeyCode.DownArrow)) {
				return CharacterDirection.DOWNLEFT;
			}
			return CharacterDirection.LEFT;
		}

		if(Input.GetKey (KeyCode.UpArrow)) {
			return CharacterDirection.UP;
		} 
		if (Input.GetKey (KeyCode.DownArrow)) {
			return CharacterDirection.DOWN;
		}
		return CharacterDirection.IDLE;

	}

	void moveCharacter (CharacterDirection characterDirection)
	{
		var mainCameraTransform = mainCamera.transform;
		Vector3 mainCamPosition = mainCameraTransform.localPosition;

		switch (characterDirection) {
			case CharacterDirection.LEFT: 
				controller.Move (new Vector3 (-walkingSpeed, 0));
				mainCamPosition.x += -walkingSpeed;
				break;
			case CharacterDirection.DOWNLEFT: 
				controller.Move (new Vector3 (-walkingSpeed, 0));
				mainCamPosition.x += -walkingSpeed;
				break;
			case CharacterDirection.UPLEFT: 
				controller.Move (new Vector3 (-walkingSpeed, 0));
				mainCamPosition.x += -walkingSpeed;
				break;
			case CharacterDirection.RIGHT: 
				controller.Move (new Vector3 (walkingSpeed, 0));
				mainCamPosition.x += walkingSpeed;
				break;
			case CharacterDirection.DOWNRIGHT: 
				controller.Move (new Vector3 (walkingSpeed, 0));
				mainCamPosition.x += walkingSpeed;
				break;
			case CharacterDirection.UPRIGHT: 
				controller.Move (new Vector3 (walkingSpeed, 0));
				mainCamPosition.x += walkingSpeed;
				break;
			default: break;	
		}
		mainCameraTransform.localPosition = mainCamPosition;

	}

	void scaleXAxisOfCharacter(float scaleOfX) {
		Vector3 theScale = transform.localScale;
		theScale.x = scaleOfX;
		transform.localScale = theScale;
	}

	void animateCharacter(CharacterAction action, CharacterDirection direction) {

		if (action == CharacterAction.ATTACK) {
			animator.Play ("Archer1_Attack2");
			return;
		}

		switch (direction) {
			case CharacterDirection.LEFT: 
				animator.Play ("Archer1_Walk 0");
				scaleXAxisOfCharacter(-1.0f);
				break;
			case CharacterDirection.DOWNLEFT:
				animator.Play ("Archer1_Walk 0");
				scaleXAxisOfCharacter(-1.0f);
				break;
			case CharacterDirection.UPLEFT:
				animator.Play ("Archer1_Jump");
				scaleXAxisOfCharacter(-1.0f);
				break;
			case CharacterDirection.RIGHT:
				animator.Play ("Archer1_Walk 0");
				scaleXAxisOfCharacter(1.0f);
				break;
			case CharacterDirection.DOWNRIGHT:
				animator.Play ("Archer1_Walk 0");
				scaleXAxisOfCharacter(1.0f);
				break;
			case CharacterDirection.UPRIGHT:
				animator.Play ("Archer1_Jump");
				scaleXAxisOfCharacter(1.0f);
				break;
			case CharacterDirection.UP:
				animator.Play ("Archer1_Jump");
				break;
			default: 
				animator.Play ("Archer1_Idle");
			return;
		}
		
	}

	// Update is called once per frame
	void Update () {
		CharacterAction characterAction = CharacterAction.NONE;

		if (Input.GetKey (KeyCode.Space)) 
		{
			characterAction = CharacterAction.ATTACK;
		}

		CharacterDirection characterDirection = determineCharacterDirection ();

		animateCharacter (characterAction, characterDirection);
		moveCharacter (characterDirection);
	}

	enum CharacterDirection
	{
		UP,
		DOWN,
		LEFT,
		RIGHT,
		UPLEFT,
		UPRIGHT,
		DOWNLEFT,
		DOWNRIGHT,
		IDLE
	}
	
	enum CharacterAction
	{
		ATTACK,
		NONE
	}

}
