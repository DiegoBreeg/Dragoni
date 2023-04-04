using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Linq;

public class PlayerMovement : MonoBehaviour
{
    private Animator anim;
    private bool isMoving;
    private Vector3 origPos, targetPos;
    private float timeToMove = 0.5f;
    private float timeToMoveModifier;
    private BoxCollider2D boxCollider;
    private float layer;

    public void Move(Vector3 direction)
    {
        layer = GetComponent<Info>().layer;
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        if (isMoving) return;
        if (!IsWalkable(direction)) return;
        StartCoroutine(MovePlayer(direction));
    }

    public IEnumerator MovePlayer(Vector3 direction)
    {
        isMoving = true;
        anim.Play("walk");
        float elapsedTime = 0;
        origPos = transform.position;
        targetPos = origPos + direction;
        while (elapsedTime < timeToMoveModifier)
        {
            transform.position = Vector3.Lerp(origPos, targetPos, (elapsedTime / timeToMoveModifier));
            yield return null;
            elapsedTime += Time.deltaTime;
        }
        transform.position = targetPos;
        isMoving = false;
        anim.Play("idle");
    }

    private bool IsWalkable(Vector3 direction)
    {
        GameObject nearestGameObject = GetNearestGameObject(direction);
        Walkable isWalkable = nearestGameObject.GetComponent<Walkable>();
        if (isWalkable == null) return false;
        timeToMoveModifier = isWalkable.speedModifier * timeToMove;
        return true;
    }

    private GameObject GetNearestGameObject(Vector3 direction)
    {
        Debug.DrawRay(boxCollider.bounds.center + direction, direction * 0.1f, Color.red);
        RaycastHit2D[] hitList = Physics2D.RaycastAll(boxCollider.bounds.center + direction, direction, 0.1f);
        RaycastHit2D hit = Array.Find(hitList, ell => ell.collider.GetComponent<Info>().layer == layer);
        if (!hit.collider)
            return gameObject;
        Debug.Log(hit.collider.transform.parent.name);
        Debug.Log(hit.collider.GetComponent<Info>().layer);
        return hit.collider.gameObject;
    }
}