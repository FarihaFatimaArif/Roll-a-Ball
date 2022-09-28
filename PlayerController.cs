using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private FloatingJoystick _joystick;

	public Material redmaterial;
	public AudioSource source;
	public AudioClip lostSoundEffect;
	public AudioClip winSoundEffect;
	public AudioClip pickupSoundEffect;
	public float speed = 0;
	public GameObject restartbtn;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
	public GameObject lostTextObject;

	private Rigidbody rb;
	private bool gameEnd;
    private float movementX;
    private float movementY;
    private int count;
	private bool lostSE;
	private float noOfPickups;
	
	Renderer rend;

	// Start is called before the first frame update

	void Start()
	{
		lostSE = false;
		rend = GetComponent<Renderer>();
		gameEnd = false;
		rb = GetComponent<Rigidbody>();
		count = 0;
		SetCountText();
		winTextObject.SetActive(false);
		lostTextObject.SetActive(false);
		restartbtn.SetActive( false);
		//restartbtn.
	}

	void FixedUpdate()
	{
		if (!gameEnd)
		{
			Vector3 movement = new Vector3(movementX, 0.0f, movementY);
			//rb.AddForce(movement * speed);
			Vector3 movePlayer = new Vector3(_joystick.Horizontal * speed, rb.velocity.y, _joystick.Vertical * speed);
			rb.velocity = movePlayer;
		}
	
    }

	void OnTriggerEnter(Collider other)
	{
		// ..and if the GameObject you intersect has the tag 'Pick Up' assigned to it..
		if (other.gameObject.CompareTag("Pickup"))
		{
			source.PlayOneShot(pickupSoundEffect);
			other.gameObject.SetActive(false);
			count = count + 1;
			SetCountText();
		}
		else if(other.gameObject.CompareTag("Cutter"))
        {
			if (!lostSE)
			{
				rend.sharedMaterial = redmaterial;
				lostTextObject.SetActive(true);
				gameEnd = true;
				restartbtn.SetActive(true);
				lostSE = true;
				source.PlayOneShot(lostSoundEffect);
			}
		}
	}

	void OnMove(InputValue value)
	{
		Vector2 v = value.Get<Vector2>();
		movementX = v.x;
		movementY = v.y;
	}

	void SetCountText()
	{
		countText.text = "Count: " + count.ToString();

		if (count >= noOfPickups)
		{
			winTextObject.SetActive(true);
			gameEnd = true;
			restartbtn.SetActive(true);
			source.PlayOneShot(winSoundEffect);
			lostSE = true;
		}
	}
    public void ResetScene()
    {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}