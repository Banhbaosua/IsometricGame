using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XpCollecter : MonoBehaviour
{
    [SerializeField] SphereCollider coll;
    [SerializeField] Transform player;
    private void OnTriggerEnter(Collider other)
    {
        var gem = other.GetComponent<Gem>(); 
        if(gem != null)
        {
            gem.FlyTowardPlayer(player);
        }
    }
}
