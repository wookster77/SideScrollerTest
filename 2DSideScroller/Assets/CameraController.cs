using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public CharacterController characterController;
	public float lengthOfXAxisBoundaryInPixels = 150.0f;
	
	Camera mainCamera;
	CameraState cameraStateCurrent;
	CameraState cameraStatePrevious;
	
	void Start () {
		mainCamera = Camera.main;
		cameraStateCurrent = CameraState.CENTRED;
		cameraStatePrevious = CameraState.CENTRED;
	}

	CameraState determineStateOfCamera (Transform mainCameraTransform, Vector3 characterPosition, float boundaryLengthInPixels)
	{

		Vector3 mainCameraPosition = mainCameraTransform.position;

		if (characterPosition.x.Equals (mainCameraPosition.x)) {
			return CameraState.CENTRED;
		}

		switch (cameraStatePrevious) {
			case CameraState.RESOLVING_TO_CHARACTER_FROM_RIGHT :
			if ( characterController.transform.localScale.x > 0) {
					return CameraState.RESOLVING_TO_CHARACTER_FROM_RIGHT;
				} else {
					return CameraState.CENTRED;
				}
			case CameraState.RESOLVING_TO_CHARACTER_FROM_LEFT :
			if ( characterController.transform.localScale.x < 0) {
					return CameraState.RESOLVING_TO_CHARACTER_FROM_LEFT;
				} else {
					return CameraState.CENTRED;
				}
		}


		float characterPositionX = characterPosition.x;
		float rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0.8f,0,0)).x;
		float leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0.2f,0,0)).x;

		if (characterPositionX >= rightBoundary) {
			return CameraState.RESOLVING_TO_CHARACTER_FROM_RIGHT;
		} else if (characterPositionX <= leftBoundary) {
			return CameraState.RESOLVING_TO_CHARACTER_FROM_LEFT;
		} else {
			return CameraState.CENTRED;
		}
	}

	void Update () {
		Transform mainCameraTransform = mainCamera.transform;
		Vector3 characterPosition = characterController.transform.localPosition;

		cameraStateCurrent = determineStateOfCamera (mainCameraTransform, characterPosition, lengthOfXAxisBoundaryInPixels);

		cameraStatePrevious = cameraStateCurrent;

		Vector3 mainCamPosition = mainCameraTransform.localPosition;
		Vector3 smoothMovementTowardsCharacter;

		switch (cameraStateCurrent) {
		case CameraState.RESOLVING_TO_CHARACTER_FROM_RIGHT:
			smoothMovementTowardsCharacter = Vector3.Lerp (mainCamPosition, characterPosition, 1.5f * Time.deltaTime); 
			mainCamPosition.x = smoothMovementTowardsCharacter.x;
			break;
		case CameraState.RESOLVING_TO_CHARACTER_FROM_LEFT:
			smoothMovementTowardsCharacter = Vector3.Lerp (mainCamPosition, characterPosition, 1.5f * Time.deltaTime); 
			mainCamPosition.x = smoothMovementTowardsCharacter.x;
			break;
		case CameraState.CENTRED:
			break;
		}
		mainCameraTransform.localPosition = mainCamPosition;
	}
}

enum CameraState {
	CENTRED,
	RESOLVING_TO_CHARACTER_FROM_RIGHT,
	RESOLVING_TO_CHARACTER_FROM_LEFT
}