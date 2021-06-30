using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace aitcHUtils.TwoDimesional
{
    public static class Vectors
    {
        public static float GetSquaredVelocity(Vector3 basePos, Vector3 finalPos) 
        {
            return (finalPos - basePos).sqrMagnitude;
        }

        public static float GetVelocity(Vector3 basePos, Vector3 finalPos)
        {
            return (finalPos - basePos).magnitude;
        }

        public static float GetNormalizedVelocity(Vector3 basePos, Vector3 finalPos)
        {
            return (finalPos - basePos).normalized.magnitude;
        }

        /// <summary>
        /// Is the distance between currentPos and targetPos is less than the mentioned gap?
        /// </summary>
        /// <param name="currentPos">Current Vector Position</param>
        /// <param name="targetPos">Target Vector Position</param>
        /// <param name="reachedGap">The minimum gap to mark as Reached</param>
        /// <returns>If the gap between two vectors is less than this, return true. Else false</returns>
        public static bool HasReached(Vector2 currentPos, Vector2 targetPos, float reachedGap = 0.05f)
        {
            return Mathf.Abs((currentPos - targetPos).sqrMagnitude) < reachedGap * reachedGap;
        }

        /// <summary>
        /// Is the distance between currentPos's position and targetPos is less than the mentioned gap?
        /// </summary>
        /// <param name="currentPos">Object's transform</param>
        /// <param name="targetPos">Target Vector Position</param>
        /// <param name="reachedGap">The minimum gap to mark as Reached</param>
        /// <returns>If the gap between two vectors is less than this, return true. Else false</returns>
        public static bool HasReached(Transform currentPos, Vector2 targetPos, float reachedGap = 0.05f)
        {
            return Mathf.Abs((currentPos.position - new Vector3(targetPos.x, targetPos.y, currentPos.position.z)).sqrMagnitude) < reachedGap * reachedGap;
        }

        /// <summary>
        /// Is the distance between currentPos's position and targetPos's position is less than the mentioned gap?
        /// </summary>
        /// <param name="currentPos">Object's Transform</param>
        /// <param name="targetPos">Target's Transform</param>
        /// <param name="reachedGap">The minimum gap to mark as Reached</param>
        /// <returns>If the gap between two objects is less than this, return true. Else false</returns>
        public static bool HasReached(Transform currentPos, Transform targetPos, float reachedGap = 0.05f)
        {
            return Mathf.Abs((currentPos.position - targetPos.position).sqrMagnitude) < reachedGap * reachedGap;
        }


        /// <summary>
        /// Returns the flipped scale according to direction 
        /// </summary>
        /// <remarks>
        /// Use L for left, R for right. Input the localScale into the currentScale parameters
        /// </remarks>
        /// <returns>Adjusted scale according to the direction</returns>
        public static Vector3 Flip2D(Vector3 currentScale)
        {
            Vector3 adjustedScale = currentScale;
            adjustedScale.x *= -1;

            return adjustedScale;
        }
    }

}

