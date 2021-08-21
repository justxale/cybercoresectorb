using UnityEngine;

namespace Player
{
	public class FollowingCamera : MonoBehaviour
	{
		public Transform objectToFollow;
		[Range(1, 10)]
		public float smoothMultiplier;
		// Start is called before the first frame update
		private void Start()
		{
        
		}

		// Update is called once per frame
		private void FixedUpdate()
		{
			transform.position = Vector3.Lerp(transform.position, objectToFollow.position, 
				smoothMultiplier*Time.deltaTime);
		}
	}
}
