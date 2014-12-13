using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CharacterController))]
public class CharacterControl2D : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		CharacterController controller = GetComponent<CharacterController>();

		if (Input.GetKey(KeyCode.RightArrow)) {
			controller.Move (new Vector3 (1, 0));
		} else if (Input.GetKey(KeyCode.LeftArrow)) {
			controller.Move (new Vector3 (-1, 0));
		}

	}
}
