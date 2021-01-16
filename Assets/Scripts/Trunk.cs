using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trunk : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool markedForDestruction = false;
    private SpriteRenderer spriteRenderer;
    public enum BranchLocation
    {
        Left,
        Right,
        None
    }
    public BranchLocation branchLocation;
    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (markedForDestruction)
        {
            var color = spriteRenderer.color;
            color = new Color(color.r, color.g, color.b, color.a-=0.05f);
            spriteRenderer.color = color;
        }
    }

    public void FlyAwayAndDestroy(CharacterSide.Side side)
    {
        markedForDestruction = true;
        
        rb.isKinematic = false;
        var xForce = side == CharacterSide.Side.Left ? 5f: -5f;
        
        rb.AddForce(new Vector3(xForce, 0f, 0f), ForceMode2D.Impulse);
        rb.AddTorque(20f);
        
        Invoke(nameof(DestroyMyself), 2f);
    }
    private void DestroyMyself()
    {
        Destroy(gameObject);
    }
}
