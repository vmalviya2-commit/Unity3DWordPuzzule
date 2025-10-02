using UnityEngine;

namespace WorddyBuddy.Splash
{
    /// <summary>
    /// Provides a gentle floating and swaying motion for balloon props.
    /// </summary>
    public class BalloonBobbing : MonoBehaviour
    {
        [Tooltip("Vertical movement amplitude.")]
        public float amplitude = 0.15f;

        [Tooltip("Speed multiplier for the bobbing motion.")]
        public float speed = 1f;

        [Tooltip("Maximum Z-axis sway angle in degrees.")]
        public float swayAmplitude = 7f;

        Vector3 _basePosition;

        void Start()
        {
            _basePosition = transform.position;
        }

        void Update()
        {
            float time = Time.time * speed;
            float verticalOffset = Mathf.Sin(time) * amplitude;

            transform.position = _basePosition + new Vector3(0f, verticalOffset, 0f);
            transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Sin(time * 0.8f) * swayAmplitude);
        }
    }
}
