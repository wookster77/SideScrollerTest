using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CharacterController))]
[RequireComponent (typeof (Animator))]
public class CharacterControl2D : MonoBehaviour {
	CharacterController characterController;
	Animator animator;
	Transform characterTransform;

	public float walkingSpeed = 0.9f;

	void Start () {
		characterController = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
		characterTransform = GetComponent<Transform> ();
	}

	CharacterDirection DetermineCharacterDirection ()
	{
		if (Input.GetKey (KeyCode.RightArrow)) {
			if(Input.GetKey (KeyCode.UpArrow)) {
				return CharacterDirection.UP_RIGHT;
			} 
			if (Input.GetKey (KeyCode.DownArrow)) {
				return CharacterDirection.DOWN_RIGHT;
			}
			return CharacterDirection.RIGHT;
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			if(Input.GetKey (KeyCode.UpArrow)) {
				return CharacterDirection.UP_LEFT;
			} 
			if (Input.GetKey (KeyCode.DownArrow)) {
				return CharacterDirection.DOWN_LEFT;
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

	void MoveCharacter (CharacterDirection characterDirection)
	{

		switch (characterDirection) {
			case CharacterDirection.LEFT: 
				characterController.Move (new Vector3 (-walkingSpeed, 0));
				break;
			case CharacterDirection.DOWN_LEFT: 
				characterController.Move (new Vector3 (-walkingSpeed, 0));
				break;
			case CharacterDirection.UP_LEFT: 
				characterController.Move (new Vector3 (-walkingSpeed, 0));
				break;
			case CharacterDirection.RIGHT: 
				characterController.Move (new Vector3 (walkingSpeed, 0));
				break;
			case CharacterDirection.DOWN_RIGHT: 
				characterController.Move (new Vector3 (walkingSpeed, 0));
				break;
			case CharacterDirection.UP_RIGHT: 
				characterController.Move (new Vector3 (walkingSpeed, 0));
				break;
			default: break;	
		}

	}

	void ScaleXAxisOfCharacter(float scaleOfX) {
		Vector3 theScale = characterTransform.localScale;
		theScale.x = scaleOfX;
		characterTransform.localScale = theScale;
	}

	void AnimateCharacter(CharacterAction action, CharacterDirection direction) {

		if (action == CharacterAction.ATTACK) {
			animator.Play ("Archer1_Attack2");
			return;
		}

		switch (direction) {
			case CharacterDirection.LEFT: 
				animator.Play ("Archer1_Walk 0");
				ScaleXAxisOfCharacter(-1.0f);
				break;
			case CharacterDirection.DOWN_LEFT:
				animator.Play ("Archer1_Walk 0");
				ScaleXAxisOfCharacter(-1.0f);
				break;
			case CharacterDirection.UP_LEFT:
				animator.Play ("Archer1_Jump");
				ScaleXAxisOfCharacter(-1.0f);
				break;
			case CharacterDirection.RIGHT:
				animator.Play ("Archer1_Walk 0");
				ScaleXAxisOfCharacter(1.0f);
				break;
			case CharacterDirection.DOWN_RIGHT:
				animator.Play ("Archer1_Walk 0");
				ScaleXAxisOfCharacter(1.0f);
				break;
			case CharacterDirection.UP_RIGHT:
				animator.Play ("Archer1_Jump");
				ScaleXAxisOfCharacter(1.0f);
				break;
			case CharacterDirection.UP:
				animator.Play ("Archer1_Jump");
				break;
			default: 
				animator.Play ("Archer1_Idle");
			return;
		}
		
	}

	void Update () {
		CharacterAction characterAction = CharacterAction.NONE;

		if (Input.GetKey (KeyCode.Space)) 
		{
			characterAction = CharacterAction.ATTACK;
		}

		CharacterDirection characterDirection = DetermineCharacterDirection ();

		AnimateCharacter (characterAction, characterDirection);
		MoveCharacter (characterDirection);
	}

	enum CharacterDirection
	{
		UP,
		DOWN,
		LEFT,
		RIGHT,
		UP_LEFT,
		UP_RIGHT,
		DOWN_LEFT,
		DOWN_RIGHT,
		IDLE
	}
	
	enum CharacterAction
	{
		ATTACK,
		NONE
	}

}
