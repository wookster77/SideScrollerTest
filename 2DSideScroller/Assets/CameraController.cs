using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public CharacterController characterController;
	public float lengthOfXAxisBoundaryInPixels = 150.0f;
	
	Camera mainCamera;
	CameraState cameraStateCurrent;
	CameraState cameraStatePrevious;
	Vector3 characterPositionPrevious;
	
	void Start () {
		mainCamera = Camera.main;
		cameraStateCurrent = CameraState.CENTRED;
		cameraStatePrevious = CameraState.CENTRED;
		characterPositionPrevious = characterController.transform.localPosition;
	}

	CameraState determineStateOfCamera (Transform mainCameraTransform, float characterXVelocity, float boundaryLengthInPixels)
	{

		if (characterXVelocity.Equals (0)) {
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

		float characterXPositionInPixels = mainCamera.WorldToScreenPoint (characterController.transform.localPosition).x;
		Vector3 mainCameraPosition = mainCameraTransform.localPosition;
		float cameraXPositionInPixels = mainCamera.WorldToScreenPoint (mainCameraPosition).x;
		float xAxisPixelWidth = mainCamera.pixelWidth;
		float rightBoundary = cameraXPositionInPixels + (xAxisPixelWidth / 2) - lengthOfXAxisBoundaryInPixels;
		float leftBoundary = cameraXPositionInPixels - (xAxisPixelWidth / 2) + lengthOfXAxisBoundaryInPixels;
		if (characterXPositionInPixels >= rightBoundary) {
			return CameraState.RESOLVING_TO_CHARACTER_FROM_RIGHT;
		} else if (characterXPositionInPixels <= leftBoundary) {
			return CameraState.RESOLVING_TO_CHARACTER_FROM_LEFT;
		} else {
			return CameraState.CENTRED;
		}
	}

	void Update () {
		Transform mainCameraTransform = mainCamera.transform;
		Vector3 characterPosition = characterController.transform.localPosition;
		float characterXVelocity = characterPosition.x - characterPositionPrevious.x;
		characterPositionPrevious = characterPosition;

		cameraStateCurrent = determineStateOfCamera (mainCameraTransform, characterXVelocity, lengthOfXAxisBoundaryInPixels);

		Debug.Log ("Current State: " + cameraStateCurrent);
		cameraStatePrevious = cameraStateCurrent;

		Vector3 mainCamPosition = mainCameraTransform.localPosition;

		Debug.Log ("character x velocity: " + characterXVelocity);
		switch (cameraStateCurrent) {
		case CameraState.RESOLVING_TO_CHARACTER_FROM_RIGHT:
			mainCamPosition.x += characterXVelocity;
			break;
		case CameraState.RESOLVING_TO_CHARACTER_FROM_LEFT:
			mainCamPosition.x += characterXVelocity;
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