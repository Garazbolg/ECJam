using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterControlller : MonoBehaviour
{
	protected Rigidbody2D rigid;
	public Transform feet;
	public float jumpForce = 20;
	public bool onGround = false;

	// Handles
	public bool leftHandle = false;
	public bool rightHandle = false;
	public bool jumpHandle = false;
	
	public Assets.Scripts.Data.Character character;

	public Animator anim;

	// Start is called before the first frame update
	protected virtual void Start()
	{
		rigid = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	protected virtual void Update()
	{
		transform.rotation = Quaternion.AngleAxis(Planet.GetAngle(transform.position), Vector3.back);
		anim.SetFloat("Speed", Vector3.Dot(rigid.velocity, transform.right) * 5);
	}

	private void FixedUpdate()
	{
		if (GameManager.instance.running)
		{
			Vector2 gravity = (Planet.instance.transform.position - transform.position).normalized;
			rigid.AddForce(gravity * Planet.instance.gravity, ForceMode2D.Force);

			//onGround = Physics2D.Raycast(feet.position, gravity, 0.1f, Planet.instance.layerMask);

			if (true)//onGround)
			{
				if (leftHandle)
				{
					rigid.AddForce(-transform.right * character.Speed);
				}

				if (rightHandle)
				{
					rigid.AddForce(transform.right * character.Speed);
				}

				if (jumpHandle)
				{
					rigid.AddForce(-gravity * jumpForce, ForceMode2D.Impulse);
				}
			}
		}
		else
		{
			rigid.Sleep();
		}
	}
}
