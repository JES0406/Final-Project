using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CrosshairManager : MonoBehaviour
{
    public static CrosshairManager instance;
    [SerializeField] private RawImage crosshairImage; // Assign the crosshair image in the Inspector
    private Camera mainCamera;

    private void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        mainCamera = Camera.main;
        crosshairImage.enabled = false; // Hide the crosshair initially
    }

    public void SetTarget(Transform target)
    {
        if (target != null)
        {
            crosshairImage.enabled = true;
            StartCoroutine(UpdateCrosshairPosition(target));
        }
        else
        {
            crosshairImage.enabled = false;
        }
    }

    private IEnumerator UpdateCrosshairPosition(Transform target)
    {
        while (target != null)
        {
            Vector3 screenPosition = mainCamera.WorldToScreenPoint(target.position);
            crosshairImage.transform.position = screenPosition;
            yield return null; // Wait for the next frame
        }

        crosshairImage.enabled = false; // Hide the crosshair if the target is destroyed or lost
    }
}
