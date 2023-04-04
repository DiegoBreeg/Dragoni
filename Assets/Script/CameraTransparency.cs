using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CameraTransparency : MonoBehaviour
{
    BoxCollider2D boxCollider;
    private float layer;
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();       

    }


    void Update()
    {
        layer = GetComponent<Info>().layer;
        Debug.DrawRay(boxCollider.bounds.center + Vector3.up, Vector3.up * 0.1f, Color.green);
        if(ColisionDetector())
            DisableObjects();
        if(!ColisionDetector())
            EnableObjects();
        
    }

    bool ColisionDetector()
    {

        RaycastHit2D[] hitListUp = Physics2D.RaycastAll(boxCollider.bounds.center + Vector3.up, Vector3.up, 0.1f);
        RaycastHit2D[] hitListLeft = Physics2D.RaycastAll(boxCollider.bounds.center + Vector3.left, Vector3.left, 0.1f);
        RaycastHit2D[] hitListDown = Physics2D.RaycastAll(boxCollider.bounds.center + Vector3.down, Vector3.down, 0.1f);
        RaycastHit2D[] hitListRight = Physics2D.RaycastAll(boxCollider.bounds.center + Vector3.right, Vector3.right, 0.1f);

        RaycastHit2D hitUp = Array.Find(hitListUp, ell => ell.collider.GetComponent<Info>().layer > layer);
        RaycastHit2D hitLeft = Array.Find(hitListLeft, ell => ell.collider.GetComponent<Info>().layer > layer);
        RaycastHit2D hitDown = Array.Find(hitListDown, ell => ell.collider.GetComponent<Info>().layer > layer);
        RaycastHit2D hitRight = Array.Find(hitListRight, ell => ell.collider.GetComponent<Info>().layer > layer);
        if (hitUp.collider != null) return true;
        if (hitLeft.collider != null) return true;
        if (hitDown.collider != null) return true;
        if (hitRight.collider != null) return true;
        return false;
    }

    void DisableObjects()
    {
        SpriteRenderer[] spritedEntitys = FindObjectsOfType<SpriteRenderer>();
        SpriteRenderer[] spritedEntitysAbove = Array.FindAll(spritedEntitys, ell => ell.GetComponent<Info>().layer > layer);
        foreach (SpriteRenderer entity in spritedEntitysAbove)
        {
            entity.enabled = false;
        }        
    }

    void EnableObjects()
    {
        SpriteRenderer[] spritedEntitys = FindObjectsOfType<SpriteRenderer>();
        foreach (SpriteRenderer entity in spritedEntitys)
        {
            entity.enabled = true;
        }        
    }
}
