using UnityEngine;
using System.Collections;
using TouchScript.Gestures;
using TouchScript.Hit;

public class InputDetector : MonoBehaviour {

	public delegate void InputHandler(InputEventType characterMovementState);
	public event InputHandler InputEvent;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnPressed (object sender, System.EventArgs e)
	{
		ITouchHit hit;
		PressGesture pressGesture = sender as PressGesture;
		if(pressGesture.GetTargetHitResult(out hit)) {
			Vector3 hitPoint = ((ITouchHit3D)hit).Point;
			Vector3 viewPortCoordinates = Camera.main.WorldToViewportPoint(hitPoint);
			if(viewPortCoordinates.x <0.5)
			{
				InputEvent(InputEventType.LEFT);
			}
			else
			{
				InputEvent(InputEventType.RIGHT);
			}
		}
	}

	void OnRelease (object sender, System.EventArgs e)
	{
		InputEvent(InputEventType.OFF);
	}

	private void OnEnable()
	{
		GetComponent<PressGesture>().Pressed += OnPressed;
		GetComponent<ReleaseGesture>().Released += OnRelease;
	}
	
	private void OnDisable()
	{
		GetComponent<PressGesture>().Pressed -= OnPressed;
		GetComponent<ReleaseGesture>().Released -= OnRelease;
	}
}

public enum InputEventType
{
	LEFT,
	RIGHT,
	OFF
}
