using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public CharacterController characterController;
	public float cameraSmoothnessCoefficient;

	Camera mainCamera;
	CameraState cameraStateCurrent;
	CameraState cameraStatePrevious;
	float characterPositionXPrevious;
	
	void Start () {
		mainCamera = Camera.main;
		cameraStateCurrent = CameraState.CENTRED;
		cameraStatePrevious = CameraState.CENTRED;
		characterPositionXPrevious = characterController.transform.position.x;
	}

	CameraState determineStateOfCamera (Transform mainCameraTransform, Vector3 characterPosition, float leftBoundaryX, float rightBoundaryX)
	{

		Vector3 mainCameraPosition = mainCameraTransform.position;

		if (characterPosition.x.Equals (characterPositionXPrevious)) {
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

		if (characterPositionX >= rightBoundaryX) {
			return CameraState.RESOLVING_TO_CHARACTER_FROM_RIGHT;
		} else if (characterPositionX <= leftBoundaryX) {
			return CameraState.RESOLVING_TO_CHARACTER_FROM_LEFT;
		} else {
			return CameraState.CENTRED;
		}
	}

	void Update () {
		Transform mainCameraTransform = mainCamera.transform;
		Vector3 characterPosition = characterController.transform.localPosition;
		Vector3 rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0.8f,0,0));
		Vector3 leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0.2f,0,0));
		cameraStateCurrent = determineStateOfCamera (mainCameraTransform, characterPosition, leftBoundary.x, rightBoundary.x);

		cameraStatePrevious = cameraStateCurrent;
		characterPositionXPrevious = characterPosition.x;

		Vector3 mainCamPosition = mainCameraTransform.localPosition;
		Vector3 smoothMovementTowardsCharacter;

		switch (cameraStateCurrent) {
		case CameraState.RESOLVING_TO_CHARACTER_FROM_RIGHT:
			smoothMovementTowardsCharacter = Vector3.Lerp (mainCamPosition, characterPosition + new Vector3(15,0,0), cameraSmoothnessCoefficient * Time.deltaTime); 
			mainCamPosition.x = smoothMovementTowardsCharacter.x;
			break;
		case CameraState.RESOLVING_TO_CHARACTER_FROM_LEFT:
			smoothMovementTowardsCharacter = Vector3.Lerp (mainCamPosition, characterPosition - new Vector3(15,0,0), cameraSmoothnessCoefficient * Time.deltaTime); 
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