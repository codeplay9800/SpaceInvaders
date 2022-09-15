using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameContainer : MonoBehaviour
{
    public static InGameContainer Instance { get; private set; }

    public List<Camera> m_cams;
    //public Camera threeD_Cam;
    public float m_shakeDuration;

    Camera currCam;
    int currCamNmber = 0;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    Vector3 originalPos;

    private void Awake()
    {
        InitSingleton();
    }

    void InitSingleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        InitCam();
    }

    void InitCam()
    {
        currCamNmber = 0;
        currCam = m_cams[currCamNmber];
        for(int i=0; i< m_cams.Count; i++)
        {
            m_cams[i].gameObject.SetActive(false);
        }
        currCam.gameObject.SetActive(true);
    }

    void SwitchCam()
    {
        currCamNmber = (currCamNmber + 1) % m_cams.Count;
        currCam = m_cams[currCamNmber];
        for (int i = 0; i < m_cams.Count; i++)
        {
            m_cams[i].gameObject.SetActive(false);
        }
        currCam.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.V))
        {
            SwitchCam();
        }
    }

    [ContextMenu("ShakeCamera")]
    public void StartShakeCamera()
    {
        StartCoroutine(ShakeCamera());
    }

    IEnumerator ShakeCamera()
    {
        float currDuration = m_shakeDuration;
        originalPos = currCam.transform.localPosition;
        while (currDuration >0)
        {
            currCam.transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            currDuration -= Time.deltaTime * decreaseFactor;
            yield return null;
        }

        currCam.transform.localPosition = originalPos;
        yield return null;
    }
}
