using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VfxBossAttack : MonoBehaviour
{
    private VisualEffect vfx;
    private float timer = 0.5f;
    private float time;

    private void Start()
    {
        vfx = GetComponent<VisualEffect>();
    }
    void Update()
    {
        time += Time.deltaTime;
        if (time >= timer)
        {
            vfx.Stop();
        }
        if (time >= timer + 3f)
        {
            Destroy(gameObject);
        }
    }
}
