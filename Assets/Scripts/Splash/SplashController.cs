using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace WorddyBuddy.Splash
{
    /// <summary>
    /// Controls the WorddyBuddy splash screen timeline including the title reveal,
    /// confetti burst, skip hint fade in, and the transition into the next scene.
    /// </summary>
    public class SplashController : MonoBehaviour
    {
        [Header("References")]
        [Tooltip("Main 3D TextMeshPro title transform.")]
        public Transform title3D;

        [Tooltip("Version label TextMeshPro component.")]
        public TextMeshPro versionText;

        [Tooltip("Confetti burst particle system that plays on reveal.")]
        public ParticleSystem confettiBurst;

        [Tooltip("Audio source used for splash sound effects.")]
        public AudioSource sfxSource;

        [Tooltip("Whoosh clip played while the title enters.")]
        public AudioClip whooshIn;

        [Tooltip("Sparkle clip played with the confetti burst.")]
        public AudioClip sparkleHit;

        [Tooltip("Canvas group controlling the visibility of the tap-to-skip hint.")]
        public CanvasGroup tapToSkip;

        [Tooltip("Scene that loads after the splash finishes or is skipped.")]
        public string nextSceneName = "Home";

        [Header("Motion Settings")]
        [Min(0.1f)]
        [Tooltip("Duration of the initial title entrance animation.")]
        public float enterDuration = 1.2f;

        [Tooltip("Amplitude of the idle floating motion after the entrance.")]
        public float idleFloatAmp = 0.08f;

        [Tooltip("Speed of the idle floating motion after the entrance.")]
        public float idleFloatSpeed = 1.2f;

        [Tooltip("Seconds to wait before automatically loading the next scene.")]
        public float idleHoldDuration = 2.2f;

        bool canSkip;
        Vector3 startPos;
        Vector3 endPos;
        float baseY;

        void Start()
        {
            if (title3D == null)
            {
                Debug.LogWarning("SplashController has no Title3D assigned.");
                enabled = false;
                return;
            }

            if (versionText != null)
            {
                versionText.text = "v" + Application.version;
            }

            startPos = title3D.position + new Vector3(0f, -2.2f, 0f);
            endPos = title3D.position;
            baseY = endPos.y;

            title3D.position = startPos;
            title3D.localScale = Vector3.one * 0.6f;
            title3D.gameObject.SetActive(true);

            StartCoroutine(RunSequence());
        }

        void Update()
        {
            if (title3D == null)
            {
                return;
            }

            float idleOffset = Mathf.Sin(Time.time * idleFloatSpeed) * idleFloatAmp;
            title3D.position = new Vector3(endPos.x, baseY + idleOffset, endPos.z);
            title3D.Rotate(0f, Mathf.Sin(Time.time * 0.7f) * 0.05f, 0f, Space.World);

            if (canSkip && (Input.touchCount > 0 || Input.GetMouseButtonDown(0)))
            {
                StartCoroutine(LoadNextScene());
            }
        }

        IEnumerator RunSequence()
        {
            yield return new WaitForSeconds(0.15f);

            if (sfxSource != null && whooshIn != null)
            {
                sfxSource.PlayOneShot(whooshIn);
            }

            float t = 0f;
            while (t < enterDuration)
            {
                t += Time.deltaTime;
                float progress = Mathf.SmoothStep(0f, 1f, t / enterDuration);

                title3D.position = Vector3.Lerp(startPos, endPos, progress);

                float overshoot = 1f + 0.15f * Mathf.Sin(progress * Mathf.PI);
                title3D.localScale = Vector3.one * Mathf.Lerp(0.6f, overshoot, progress);

                yield return null;
            }

            title3D.position = endPos;
            title3D.localScale = Vector3.one;

            if (confettiBurst != null)
            {
                confettiBurst.Play();
            }

            if (sfxSource != null && sparkleHit != null)
            {
                sfxSource.PlayOneShot(sparkleHit);
            }

            if (tapToSkip != null)
            {
                StartCoroutine(FadeCanvasGroup(tapToSkip, 0f, 1f, 0.6f));
            }

            canSkip = true;

            float elapsed = 0f;
            while (elapsed < idleHoldDuration)
            {
                elapsed += Time.deltaTime;
                yield return null;
            }

            StartCoroutine(LoadNextScene());
        }

        IEnumerator LoadNextScene()
        {
            if (!canSkip)
            {
                yield break;
            }

            canSkip = false;

            if (tapToSkip != null)
            {
                yield return FadeCanvasGroup(tapToSkip, tapToSkip.alpha, 0f, 0.3f);
            }

            yield return new WaitForSeconds(0.1f);

            if (!string.IsNullOrEmpty(nextSceneName))
            {
                SceneManager.LoadScene(nextSceneName);
            }
        }

        IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float startAlpha, float endAlpha, float duration)
        {
            float elapsed = 0f;
            canvasGroup.alpha = startAlpha;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float progress = Mathf.Clamp01(elapsed / duration);
                canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, progress);
                yield return null;
            }

            canvasGroup.alpha = endAlpha;
        }
    }
}
