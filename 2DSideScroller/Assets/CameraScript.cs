using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public CharacterControl2D characterController;
	Transform characterTransform;
	Camera mainCamera;
	CameraState cameraState;

	float walkingSpeed;

	// Use this for initialization
	void Start () {
		walkingSpeed = characterController.walkingSpeed;
		mainCamera = Camera.main;
		cameraState = CameraState.CENTRED;
	}
	
	// Update is called once per frame
	void Update () {
		updateCamera ();
	}

	void updateCamera ()
	{
		float boundaryLengthInPixels = 150.0f;
		
		Vector3 characterPosition = characterController.transform.localPosition;
		float characterXPositionInPixels = mainCamera.WorldToScreenPoint(characterPosition).x;
		
		Transform mainCameraTransform = mainCamera.transform;
		Vector3 mainCameraPosition = mainCameraTransform.localPosition;
		float cameraXPositionInPixels = mainCamera.WorldToScreenPoint(mainCameraPosition).x;
		float xAxisPixelWidth = mainCamera.pixelWidth;
		
		float rightBoundary = cameraXPositionInPixels + (xAxisPixelWidth/2) - boundaryLengthInPixels;
		float leftBoundary = cameraXPositionInPixels - (xAxisPixelWidth/2) + boundaryLengthInPixels;
		
		if (characterXPositionInPixels >= rightBoundary) {
			cameraState = CameraState.RESOLVING_TO_CHARACTER_FROM_RIGHT;
		} else if (characterXPositionInPixels <= leftBoundary) {
			cameraState = CameraState.RESOLVING_TO_CHARACTER_FROM_LEFT;
		} else {
			cameraState = CameraState.CENTRED;
		}
		
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