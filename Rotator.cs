using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour
{
	[SerializeField] float speed;
	[SerializeField] Vector3 rotating;
	void Update()
	{
		transform.Rotate(rotating *speed * Time.deltaTime);
	}
}