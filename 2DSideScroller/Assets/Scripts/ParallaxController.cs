using UnityEngine;
using System.Collections;

public class ParallaxController : MonoBehaviour {

	Transform backgroundTransform;
	Camera mainCamera;
	Vector3 cameraPreviousPosition;
	public float cameraFollowPercentage = 100.0f;
	Vector3 cameraVelocity;
	// Use this for initialization
	void Start () {
		backgroundTransform = GetComponent<Transform> ();
		mainCamera = Camera.main;
		cameraPreviousPosition = mainCamera.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 cameraVelocity = GetCameraVelocity ();
		Vector3 backgroundPosition = backgroundTransform.position;
		backgroundPosition.x += cameraVelocity.x*(cameraFollowPercentage/100.0f);
		backgroundTransform.position = backgroundPosition;
	}

	Vector3 GetCameraVelocity()
	{
		Vector3 cameraCurrentPosition = mainCamera.transform.position;
		cameraVelocity = cameraCurrentPosition - cameraPreviousPosition;
		cameraPreviousPosition = cameraCurrentPosition;
		return cameraVelocity;
	}
}
