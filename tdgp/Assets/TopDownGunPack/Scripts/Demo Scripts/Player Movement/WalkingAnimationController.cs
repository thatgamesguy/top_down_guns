using UnityEngine;
using System.Collections;

namespace TDGP.Demo
{
/// <summary>
/// Updates walking animation based on movement speed.
/// </summary>
	[RequireComponent (typeof(Animator))]
	public class WalkingAnimationController : MonoBehaviour
	{
		private Animator _animator;

		private int speedHash = Animator.StringToHash ("speed");
		
		private Vector2 lastPosition;
		
		void Awake ()
		{
			_animator = GetComponent<Animator> ();
		}
		
		void Start ()
		{
			lastPosition = transform.position;
		}

		void FixedUpdate ()
		{
			var velocity = (((Vector2)transform.position - lastPosition)).magnitude;



            if (velocity > 0.02f)
            { 
                var currentSpeed = Mathf.Abs(velocity) / Time.deltaTime;
                _animator.SetFloat(speedHash, currentSpeed);
            }
            else
            {
                _animator.SetFloat(speedHash, 0f);
            }
			
			lastPosition = transform.position;
		}

	}
}

