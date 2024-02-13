using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	public Rigidbody2D rigid;
	public Animator animator;
	public SpriteRenderer render;


	public float moveSpeed;
	public float breakPower;
	public float maxXSpeed;
	public float maxYSpeed;

	public float jumpPower;

	private bool isGround;
	Vector2 moveDir;

	private void FixedUpdate()
	{
		Move();
	}

	private void Move()
	{
		if (moveDir.x < 0 && rigid.velocity.x > -maxXSpeed)
		{
			rigid.AddForce(Vector2.right * moveSpeed * moveDir.x);
		}

		else if (moveDir.x > 0 && rigid.velocity.x < maxXSpeed)
		{
			rigid.AddForce(Vector2.right * moveSpeed * moveDir.x);
		}

		else if (moveDir.x == 0 && rigid.velocity.x > 0.1f)
		{
			rigid.AddForce(Vector2.left * breakPower);
		}

		else if (moveDir.x == 0 && rigid.velocity.x < -0.1f)
		{
			rigid.AddForce(Vector2.right * breakPower);
		}

		if (rigid.velocity.y < -maxYSpeed)
		{
			Vector2 velocity = rigid.velocity;
			velocity.y = -maxYSpeed;
			rigid.velocity = velocity;
		}

		animator.SetFloat("YSpeed", rigid.velocity.y);
	}

	private void Jump()
	{
		Vector2 velocity = rigid.velocity;
		velocity.y = jumpPower;
		rigid.velocity = velocity;
	}
	private void OnMove(InputValue value)
	{
		moveDir = value.Get<Vector2>();

		if (moveDir.x < 0)
		{
			render.flipX = true;
			animator.SetBool("Run", true);
		}
		else if (moveDir.x > 0)
		{
			render.flipX = false;
			animator.SetBool("Run", true);
		}
		else
		{
			animator.SetBool("Run", false);
		}


	}

	private void OnJump(InputValue value)
	{
		if (value.isPressed && isGround)
		{
			Jump();
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		isGround = true;
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		isGround = false;
	}
}
