using UnityEngine;
using System.Collections;

/// <summary>
/// Destroys the attached object after "lifetime" seconds have passed
/// </summary>
public class DestroyInTime : MonoBehaviour
{
	public float lifetime = 1.0f;

	// Use this for initialization
	void Start()
	{
	
	}
	
	// Update is called once per frame
	void Update()
	{
		lifetime -= Time.deltaTime;

		if (lifetime <= 0) {
			Destroy(this.gameObject);
		}
	}
}
