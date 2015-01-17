using UnityEngine;
using System.Collections;
using TouchScript.Gestures;

[RequireComponent (typeof (CharacterController))]
[RequireComponent (typeof (Animator))]
public class CharacterControl2D : MonoBehaviour {
	public InputDetector inputDetector;
	CharacterController characterController;
	Animator animator;
	Transform characterTransform;
	CharacterDirection characterDirection;
	public float walkingSpeed = 0.9f;

	InputEventType lastInputEvent = InputEventType.OFF;


	void Start () {
		characterController = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
		characterTransform = GetComponent<Transform> ();
		characterDirection = CharacterDirection.IDLE;

		inputDetector.InputEvent += OnInputEvent;
	}

	void OnInputEvent (InputEventType eventType)
	{
		this.lastInputEvent = eventType;
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
			animator.SetInteger("CHARACTER_STATE",0);
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

		switch (this.characterDirection) {
		
		case CharacterDirection.IDLE:
			if(this.lastInputEvent.Equals(InputEventType.LEFT)) {
				this.characterDirection = CharacterDirection.LEFT;
				break;
			}
			if(this.lastInputEvent.Equals(InputEventType.RIGHT)) {
				this.characterDirection = CharacterDirection.RIGHT;
				break;
			}
			break;
		
		case CharacterDirection.LEFT: 
			if(this.lastInputEvent.Equals(InputEventType.OFF)) {
				this.characterDirection = CharacterDirection.IDLE;
				break;
			}
			if(this.lastInputEvent.Equals(InputEventType.RIGHT)) {
				this.characterDirection = CharacterDirection.RIGHT;
				break;
			}
			break;
				
		case CharacterDirection.RIGHT:
			if(this.lastInputEvent.Equals(InputEventType.LEFT)) {
				this.characterDirection = CharacterDirection.LEFT;
				break;
			}
			if(this.lastInputEvent.Equals(InputEventType.OFF)) {
				this.characterDirection = CharacterDirection.IDLE;
				break;
			}
			break;
			default: throw new UnityException("Character Direction state: " + this.characterDirection + " has not been accounted for.");
		}

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
