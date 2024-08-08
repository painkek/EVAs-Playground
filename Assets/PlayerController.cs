using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;

    //animator script
    public Animator animator;
    public ContactFilter2D movementFilter;

    Vector2 movementInput;
    Rigidbody2D rb;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        animator.SetFloat("Horizontal", movementInput.x);
        animator.SetFloat("Vertical", movementInput.y);
        animator.SetFloat("Speed", movementInput.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        if (movementInput != Vector2.zero)
        {
            bool success = tryMove(movementInput);
            //slide if we can't move
            if (!success)
            {
                success = tryMove(new Vector2(movementInput.x, 0));

                if (!success)
                {
                    success = tryMove(new Vector2(0, movementInput.y));
                }
            }
        }
    }

    private bool tryMove(Vector2 direction)
    {
        int cound = rb.Cast(
               direction,
               movementFilter,
               castCollisions,
               moveSpeed * Time.fixedDeltaTime + collisionOffset);

        if (cound == 0)
        {
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            return true;
        }
        else
        {
            return false;
        }

    }


    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }
}
