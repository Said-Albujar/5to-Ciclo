using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;

public class Sensibility : MonoBehaviour
{
    [Header("BarraSliderX")]
    public Slider sliderX;
    public float slidervalueX;
    public TMP_Text textValueX;

    [Header("BarraSliderY")]
    public Slider sliderY;
    public float slidervalueY;
    public TMP_Text textValueY;


    [Header("CamaraGeneral")]
    [SerializeField]
    private GameObject cameraGeneral;
    private CinemachineFreeLook CinemachineFreeLookGeneral;

    public static Sensibility instance;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        if (!CinemachineFreeLookGeneral)
        {
            CinemachineFreeLookGeneral = cameraGeneral.GetComponent<CinemachineFreeLook>();
        }


        sliderX.value = PlayerPrefs.GetFloat("SensibilityX", slidervalueX);
        sliderY.value = PlayerPrefs.GetFloat("SensibilityY", slidervalueY);
        CinemachineFreeLookGeneral.m_YAxis.m_MaxSpeed = sliderY.value / (sliderY.maxValue * 100); 
        CinemachineFreeLookGeneral.m_XAxis.m_MaxSpeed = sliderX.value * 0.8f;

        sliderX.value = sliderX.maxValue / 2;
        sliderY.value = sliderY.maxValue / 2;
    }

    // Update is called once per frame
    void Update()
    {

        if (!CinemachineFreeLookGeneral)
        {
            CinemachineFreeLookGeneral = cameraGeneral.GetComponent<CinemachineFreeLook>();
            sliderX.value = PlayerPrefs.GetFloat("SensibilityX", slidervalueX);
            sliderY.value = PlayerPrefs.GetFloat("SensibilityY", slidervalueY);
            CinemachineFreeLookGeneral.m_YAxis.m_MaxSpeed = sliderY.value / (sliderY.maxValue * 100);
            CinemachineFreeLookGeneral.m_XAxis.m_MaxSpeed = sliderX.value * 0.8f;
        }

        EnableCamera();

    }


    public void ChangeSliderX(float value)
    {
        slidervalueX = value;
        PlayerPrefs.SetFloat("SensibilityX", slidervalueX);

        CinemachineFreeLookGeneral.m_XAxis.m_MaxSpeed = sliderX.value * 0.8f;

        ShowValue();

    }

    public void ChangeSliderY(float value)
    {
        slidervalueY = value;
        PlayerPrefs.SetFloat("SensibilityY", slidervalueY);

        CinemachineFreeLookGeneral.m_YAxis.m_MaxSpeed = sliderY.value / (sliderY.maxValue * 100);

        ShowValue();

    }

    public void EnableCamera()
    {
        CinemachineFreeLookGeneral.m_YAxis.m_MaxSpeed = sliderY.value / (sliderY.maxValue * 100);
        CinemachineFreeLookGeneral.m_XAxis.m_MaxSpeed = sliderX.value * 0.8f;
    }


    public void ShowValue()
    {
        float distanceFromMin = (sliderX.value - sliderX.minValue);
        float sliderRangeX = (sliderX.maxValue - sliderX.minValue);
        float sliderPercent = (distanceFromMin / sliderRangeX);
        textValueX.text = sliderPercent.ToString("F2");

        float distanceFromMinAim = (sliderY.value - sliderY.minValue);
        float sliderRangeY = (sliderY.maxValue - sliderY.minValue);
        float sliderPercentAim = (distanceFromMinAim / sliderRangeY);
        textValueY.text = sliderPercentAim.ToString("F2");
    }
}
