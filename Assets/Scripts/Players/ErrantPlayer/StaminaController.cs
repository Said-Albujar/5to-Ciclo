using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaController : MonoBehaviour
{
    private PlayerMovement errantMovement;
    private CollectObject recolectar;


    [Header("Stamina Main Parameters")]
    public static float staminaActual;
    [SerializeField] float staminaMax;
    [HideInInspector] public float regenTimer;
    public float staminaNeedle;
    public static bool canRun;

    public bool mejora;

    [Header("Stamina Regen Parameters")]
    [Range(0, 50)] [SerializeField] float staminaDrain = 0.5f;
    [Range(0, 50)] [SerializeField] float staminaRegen = 0.5f;

    [Header("Stamina UI Elements")]
    [SerializeField] Image imageStamina;
    [SerializeField] CanvasGroup staminaGroup;

    private bool once;

    private void Awake()
    {
        errantMovement = GetComponent<PlayerMovement>();
        recolectar = GetComponent<CollectObject>();  
    }
    void Start()
    {
        staminaActual = staminaMax;
        staminaNeedle = staminaMax / 4;
    }

    // Update is called once per frame
    void Update()
    {
        staminaActual = Mathf.Clamp(staminaActual, -1f, staminaMax);
        StaminaNeedle();

        if (errantMovement.actualSpeed >= errantMovement.runSpeed || staminaActual >= staminaMax)
        {
            regenTimer = 0f;
        }

        if (!errantMovement.isRunning)
        {
            if (staminaActual <= staminaMax)
            {
                Test();
                CheckStamina(1);
            }

            if (staminaActual >= staminaMax)
            {
                CheckStamina(0);

            }
        }

        IsRunning();
        isGliding();
        if (recolectar.botonPresionado == true && !mejora)
        {
            staminaMax = 100;
            staminaRegen = 30f;
            mejora = true;
        }

    }
    public void isGliding()
    {
        if(errantMovement.currentstate==PlayerMovement.state.gliding)
        {
            staminaActual -= staminaDrain * Time.deltaTime;
            CheckStamina(1);
            if (staminaActual <= 0)
            {
                errantMovement.CanGlide();
            }
        }
    }

    public void IsRunning()
    {
        if (errantMovement.actualSpeed >= errantMovement.runSpeed && errantMovement.grounded)
        {
            staminaActual -= staminaDrain * Time.deltaTime;
            CheckStamina(1);
            if (staminaActual <= 0)
            {
                canRun = false;
            }
        }
    }

    void CheckStamina(int value)
    {
        imageStamina.fillAmount = staminaActual / staminaMax;

        if (value <= 0)
            staminaGroup.alpha = 0;
        else
            staminaGroup.alpha = 1;

    }

    void Test()
    {
        regenTimer += Time.fixedDeltaTime;
        if (regenTimer >= 2f)
        {
            staminaActual += staminaRegen * Time.deltaTime;
            CheckStamina(1);
        }
    }
    void StaminaNeedle()
    {
        if (staminaActual >= staminaNeedle) 
        {
            canRun = true;

            if (!once)
            {
                once = true;

            }
        }

        else
        {
            once = false;

        }

    }

}
