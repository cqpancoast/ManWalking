using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float walkSpeed;
	public float runSpeed;
	public float jumpVelocity;
	public float gravityScale;
	public float gravityFallFactor;
	public int jumpsAllowed;
	
	public CharacterController controller;
	public new Transform camera;
	
	private Vector3 moveDirection;
	private float moveSpeed;
	private float playerAngle;
	private bool isRunning;
	private int jumpsRemaining;
	private float activeGravityScale;
	
	public GameObject ledgeGrabbed; //See above. Is there a way to deal with this
	public float ledgeRotation;
	public bool isHanging; //I like this being accessible by LedgeBehavior but I don't like it being Unity editable...
	public float hangXPosition, hangYPosition, hangZPosition; // :c
	public bool playerFrozen;
	
	public Animator anim;
	private static readonly int IsHanging = Animator.StringToHash("isHanging");
	private static readonly int NormSpeed = Animator.StringToHash("normSpeed");
	private static readonly int IsRunning = Animator.StringToHash("isRunning");
	private static readonly int IsJumping = Animator.StringToHash("IsJumping");


	private void Start() { 
		controller = GetComponent<CharacterController>();
		camera = FindObjectOfType<Camera>().transform;
		anim = transform.GetChild(0).gameObject.GetComponent<Animator>();
	}

	private void Jump() {

		if (jumpsRemaining > 0) {
			moveDirection.y = jumpVelocity;
			jumpsRemaining -= 1;
		}

	}

	// At some point, if necessary, I can TOTALLY clean this up.
	private void Update() {

		if (playerFrozen)
			return;
		
		if (isHanging) {

			// Snap player into position
			transform.position = ledgeGrabbed.transform.TransformPoint(hangXPosition, hangYPosition, hangZPosition);
			transform.rotation = Quaternion.Euler(0f, ledgeRotation, 0f);

			moveDirection = new Vector3(0f, 0f, 0f);
			if (Input.GetButtonDown("Jump")
			    || Input.GetButtonDown("Horizontal")
			    || Input.GetButtonDown("Vertical")) {
				moveDirection.y = jumpVelocity;
			}
			return;
		}

		// Determine move speed
		isRunning = Input.GetButton("Shift L");
		moveSpeed = isRunning ? runSpeed : walkSpeed;

		// Normalize x-z movement input
		Vector3 inputNorm = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")).normalized;
		// Rotate movement axes by camera rotation usin' a lil' ~linear algebra~
		float cameraAngle = -Mathf.Deg2Rad * camera.rotation.eulerAngles.y;
		moveDirection = new Vector3(inputNorm.x * moveSpeed * Mathf.Cos(cameraAngle) -
		                            inputNorm.z * moveSpeed * Mathf.Sin(cameraAngle),
			moveDirection.y,
			inputNorm.z * moveSpeed * Mathf.Cos(cameraAngle) +
			inputNorm.x * moveSpeed * Mathf.Sin(cameraAngle));

		// If the user is providing movement input, turn the player in the direction of movement
		if (Input.GetButton("Vertical") || Input.GetButton("Horizontal")) {
			transform.LookAt(new Vector3(moveDirection.x, 0f, moveDirection.z) + transform.position);
		}

		// Make sure velocity due to gravity doesn't increase without check, restore jumps when on ground
		if (controller.isGrounded) {
			moveDirection.y = 0f;
			jumpsRemaining = jumpsAllowed;
		}

		// Jump!
		if (Input.GetButtonDown("Jump")) {
			Jump();
		}

		// Fall!
		activeGravityScale = Input.GetButton("Jump") ? gravityScale : gravityScale * gravityFallFactor;

		moveDirection.y += Physics.gravity.y * activeGravityScale * Time.deltaTime;

		controller.Move(motion: moveDirection * Time.deltaTime);

		// Set animator variables
		if (!PauseMenu.gamePaused) {
			anim.SetBool(IsJumping, !controller.isGrounded);
		}
		anim.SetBool(IsRunning, isRunning);
		anim.SetFloat(NormSpeed, inputNorm.magnitude);
		
	}

	public IEnumerator Hang() {
		
		anim.SetBool(IsHanging, true);
		isHanging = true;
		
		yield return new WaitUntil(() => Input.anyKeyDown && !Input.GetKeyDown("left shift"));
		
		anim.SetBool(IsHanging, false);
		isHanging = false;

	}
	
}