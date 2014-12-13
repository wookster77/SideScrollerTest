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
		controller.Move(new Vector3(1,0));

	}
}
