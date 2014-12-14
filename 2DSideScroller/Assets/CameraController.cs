using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public CharacterControl2D characterController;
	public float lengthOfXAxisBoundaryInPixels = 150.0f;

	Transform characterTransform;
	Camera mainCamera;
	CameraState cameraState;
	float walkingSpeed;

	
	void Start () {
		walkingSpeed = characterController.walkingSpeed;
		mainCamera = Camera.main;
		cameraState = CameraState.CENTRED;
	}

	CameraState determineStateOfCamera (Transform mainCameraTransform, float boundaryLengthInPixels)
	{
		Vector3 characterPosition = characterController.transform.localPosition;
		float characterXPositionInPixels = mainCamera.WorldToScreenPoint (characterPosition).x;
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
		cameraState = determineStateOfCamera (mainCameraTransform, lengthOfXAxisBoundaryInPixels);

		Vector3 mainCamPosition = mainCameraTransform.localPosition;
		switch (cameraState) {
		case CameraState.RESOLVING_TO_CHARACTER_FROM_RIGHT:
			mainCamPosition.x += walkingSpeed;
			break;
		case CameraState.RESOLVING_TO_CHARACTER_FROM_LEFT:
			mainCamPosition.x -= walkingSpeed;
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