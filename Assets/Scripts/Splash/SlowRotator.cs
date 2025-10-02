using UnityEngine;

namespace WorddyBuddy.Splash
{
    /// <summary>
    /// Applies a constant rotation to create subtle prop motion.
    /// </summary>
    public class SlowRotator : MonoBehaviour
    {
        [Tooltip("Rotation amount in degrees per second for each axis.")]
        public Vector3 eulerPerSecond = new Vector3(0f, 15f, 0f);

        void Update()
        {
            transform.Rotate(eulerPerSecond * Time.deltaTime, Space.World);
        }
    }
}
