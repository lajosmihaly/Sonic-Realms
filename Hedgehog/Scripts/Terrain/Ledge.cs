﻿using UnityEngine;

namespace Hedgehog.Terrain
{
    /// <summary>
    /// Turns a platform into a ledge where a controller can only collide with its top side.
    /// </summary>
    [RequireComponent(typeof(PlatformTrigger))]
    public class Ledge : MonoBehaviour
    {
        public void OnEnable()
        {
            GetComponent<PlatformTrigger>().CollisionPredicates.Add(CollisionPredicate);
        }

        public void OnDisable()
        {
            GetComponent<PlatformTrigger>().CollisionPredicates.Remove(CollisionPredicate);
        }

        // The platform can be collided with if the player is checking its bottom side and
        // the result of the check did not stop where it started.
        public static bool CollisionPredicate(TerrainCastHit hit)
        {
            if(hit.Source == null) 
                return (hit.Side & TerrainSide.Bottom) > 0;
            
            // Check must be coming from player's bottom side and be close to the top
            // of the platform
            return (hit.Side & TerrainSide.Bottom) > 0 && hit.Hit.fraction > 0.0f;
        }
    }
}
