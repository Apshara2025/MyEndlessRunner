using System.Collections;
using TMPro;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class SpeedUpCinemachineCamera : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] float multiplier = 2f;
    [SerializeField] float minFOV = 31.9f; 
    [SerializeField] float maxFOV = 87.5f ;
    [SerializeField] float zoomDuration = 3f;
    [SerializeField] float currentFOV;
    CinemachineCamera cinemachineCamera;
    


    void Awake()
    {
        cinemachineCamera = GetComponent<CinemachineCamera>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeFOV(float speedChange) {
        StartCoroutine(ChangeFOVCoroutine(speedChange));
    }

    // lerping from one value to another -> use coroutine

    IEnumerator ChangeFOVCoroutine(float speedChange)
    {
        float startFOV = cinemachineCamera.Lens.FieldOfView;
        float targetFOV = startFOV + speedChange * multiplier;
        float actualFOV = Mathf.Clamp(targetFOV, minFOV, maxFOV);

        float elapsedTime = 0f;
        while (elapsedTime < zoomDuration)
        {
            float t = elapsedTime / zoomDuration;
            elapsedTime += Time.deltaTime;
            currentFOV = Mathf.Lerp(startFOV, actualFOV, t);
            cinemachineCamera.Lens.FieldOfView = currentFOV;

            yield return null;
        }
        cinemachineCamera.Lens.FieldOfView = actualFOV;
    }
}
