using UnityEngine;
using System.Collections;
using TouchScript.Gestures;

public class CameraController : MonoBehaviour {

	public CharacterController characterController;
	public float cameraSmoothnessCoefficient;

	Camera mainCamera;
	CameraState cameraStateCurrent;
	CameraState cameraStatePrevious;
	float characterPositionXPrevious;
	Vector3 distanceThatCameraNeedsToMove;
	
	void Start () {

		mainCamera = Camera.main;
		cameraStateCurrent = CameraState.CENTRED;
		cameraStatePrevious = CameraState.CENTRED;
		characterPositionXPrevious = characterController.transform.position.x;
		distanceThatCameraNeedsToMove = new Vector3(Camera.main.ViewportToWorldPoint (new Vector3 (0.5f, 0, 0)).x - Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, 0)).x, 0, 0);
	}

	CameraState DetermineCameraState (Transform mainCameraTransform, Vector3 characterPosition, float leftBoundaryX, float rightBoundaryX) {

		if (characterPosition.x.Equals (characterPositionXPrevious)) {
			return CameraState.CENTRED;
		}

		switch (cameraStatePrevious) {
			case CameraState.RESOLVING_TO_CHARACTER_MOVING_RIGHT :
			if ( characterController.transform.localScale.x > 0) {
					return CameraState.RESOLVING_TO_CHARACTER_MOVING_RIGHT;
				} else {
					return CameraState.CENTRED;
				}
			case CameraState.RESOLVING_TO_CHARACTER_MOVING_LEFT :
			if ( characterController.transform.localScale.x < 0) {
					return CameraState.RESOLVING_TO_CHARACTER_MOVING_LEFT;
				} else {
					return CameraState.CENTRED;
				}
		}


		float characterPositionX = characterPosition.x;

		if (characterPositionX >= rightBoundaryX) {
			return CameraState.RESOLVING_TO_CHARACTER_MOVING_RIGHT;
		} else if (characterPositionX <= leftBoundaryX) {
			return CameraState.RESOLVING_TO_CHARACTER_MOVING_LEFT;
		} else {
			return CameraState.CENTRED;
		}
	}

	void Update () {
		Transform mainCameraTransform = mainCamera.transform;
		Vector3 characterPosition = characterController.transform.position;
		Vector3 rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0.8f,0,0));
		Vector3 leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0.2f,0,0));
		cameraStateCurrent = DetermineCameraState (mainCameraTransform, characterPosition, leftBoundary.x, rightBoundary.x);

		cameraStatePrevious = cameraStateCurrent;
		characterPositionXPrevious = characterPosition.x;

		Vector3 mainCamPosition = mainCameraTransform.position;
		Vector3 smoothMovementTowardsCharacter;

		switch (cameraStateCurrent) {
		case CameraState.RESOLVING_TO_CHARACTER_MOVING_RIGHT:
			smoothMovementTowardsCharacter = Vector3.Lerp (mainCamPosition, characterPosition + distanceThatCameraNeedsToMove, cameraSmoothnessCoefficient * Time.deltaTime); 
			mainCamPosition.x = smoothMovementTowardsCharacter.x;
			break;
		case CameraState.RESOLVING_TO_CHARACTER_MOVING_LEFT:
			smoothMovementTowardsCharacter = Vector3.Lerp (mainCamPosition, characterPosition - distanceThatCameraNeedsToMove, cameraSmoothnessCoefficient * Time.deltaTime); 
			mainCamPosition.x = smoothMovementTowardsCharacter.x;
			break;
		case CameraState.CENTRED:
			break;
		}
		mainCameraTransform.position = mainCamPosition;
	}
}

enum CameraState {
	CENTRED,
	RESOLVING_TO_CHARACTER_MOVING_RIGHT,
	RESOLVING_TO_CHARACTER_MOVING_LEFT
}