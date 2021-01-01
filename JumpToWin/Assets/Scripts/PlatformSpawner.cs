using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public static PlatformSpawner instance;

    [SerializeField] private GameObject player;

    [Space(20f)]
    [SerializeField] private GameObject parentPlatform;
    [SerializeField] private GameObject platform;

    private float minX = -2.25f;
    private float maxX = 2.25f;
    private float minY = -5.0f;
    private float maxY = -3.6f;

    private bool lerpCamera;
    private float lerpX;
    private float lerpTime = 1.5f;

    private void Awake()
    {
        SetInstance();
    }

    private void SetInstance()
    {
        if (instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        InitializeGame();
    }

    private void Update()
    {
        if (lerpCamera)
        {
            LerpTheCamera();
        }
    }

    private void InitializeGame()
    {
        Vector3 temp = new Vector3(Random.Range(minX, minX + 1.5f), Random.Range(minY, maxY), 0);

        Instantiate(platform, temp, Quaternion.identity, parentPlatform.transform);

        temp.y += 3f;
        Instantiate(player, temp, Quaternion.identity);

        temp = new Vector3(Random.Range(maxX, maxX - 1f), Random.Range(minY, maxY), 0);

        Instantiate(platform, temp, Quaternion.identity, parentPlatform.transform);
    }

    private void LerpTheCamera()
    {
        float cameraX = Camera.main.transform.position.x;

        cameraX = Mathf.Lerp(cameraX, lerpX, lerpTime * Time.deltaTime);

        Camera.main.transform.position = new Vector3(cameraX, Camera.main.transform.position.y, Camera.main.transform.position.z);
        if (Camera.main.transform.position.x >= (lerpX - 0.07f))
        {
            lerpCamera = false;
        }
    }

    public void CreatePlatformAndLerp(float lerpPosition)
    {
        CreateNewPlatform();

        lerpX = lerpPosition + maxX;

        lerpCamera = true;
    }

    private void CreateNewPlatform()
    {
        float cameraX = Camera.main.transform.position.x;

        float newMaxX = (maxX * 2f) + cameraX;

        Instantiate(platform, new Vector3(Random.Range(newMaxX, newMaxX - 1f), Random.Range(maxY, maxY - 1.2f), 0), Quaternion.identity, parentPlatform.transform);
    }
}