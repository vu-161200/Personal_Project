using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public CapsuleCollider characterCollider;
    public CapsuleCollider characterCollisionBlockerCollider;
    
    private void Start() {
        Physics.IgnoreCollision(characterCollider, characterCollisionBlockerCollider, true);
    }
}
