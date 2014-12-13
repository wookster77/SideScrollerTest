﻿using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CharacterController))]
[RequireComponent (typeof (Animator))]
public class CharacterControl2D : MonoBehaviour {
	CharacterController characterController;
	Animator animator;
	Transform characterTransform;
	Camera mainCamera;
	CameraState cameraState;

	public float walkingSpeed = 0.9f;

	// Use this for initialization
	void Start () {
		characterController = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
		characterTransform = GetComponent<Transform> ();
		mainCamera = Camera.main;
	}

	CharacterDirection determineCharacterDirection ()
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

	void moveCameraAlongXAxis(float xAxisTransform) {
//		var mainCameraTransform = mainCamera.characterTransform;
//		Vector3 mainCamPosition = mainCameraTransform.localPosition;
//
//		mainCamPosition.x += xAxisTransform;
	}

	void moveCharacter (CharacterDirection characterDirection)
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

	void scaleXAxisOfCharacter(float scaleOfX) {
		Vector3 theScale = characterTransform.localScale;
		theScale.x = scaleOfX;
		characterTransform.localScale = theScale;
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
			case CharacterDirection.DOWN_LEFT:
				animator.Play ("Archer1_Walk 0");
				scaleXAxisOfCharacter(-1.0f);
				break;
			case CharacterDirection.UP_LEFT:
				animator.Play ("Archer1_Jump");
				scaleXAxisOfCharacter(-1.0f);
				break;
			case CharacterDirection.RIGHT:
				animator.Play ("Archer1_Walk 0");
				scaleXAxisOfCharacter(1.0f);
				break;
			case CharacterDirection.DOWN_RIGHT:
				animator.Play ("Archer1_Walk 0");
				scaleXAxisOfCharacter(1.0f);
				break;
			case CharacterDirection.UP_RIGHT:
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
