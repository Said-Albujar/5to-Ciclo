using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break : MonoBehaviour
{
    public bool CanBreak = false;
    public ChangeCharacter Change;
    public GameObject Rock;
    [SerializeField] GameObject pointsPrefab;
    [SerializeField] int RockValueInPoints;
    bool spawnedPoints = false;
    public GameObject brokenVfx;

    void Start()
    {
        Change = FindObjectOfType<ChangeCharacter>();
    }
    void Update()
    {
        if (CanBreak == true && Input.GetKeyDown(KeyCode.E) && Change.IsMiner == true && Change.anim.canUse)
        {
            Change.anim.UsingToolAnim(1);//0.alicate, 1.pico, 2.tijeras
            if (!spawnedPoints) SpawnPoints();
            AudioManager.Instance.PlaySFX("DestroyRock");
            Destroy(Rock);
            InstantiateVFX();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Miner"))
        {
            CanBreak = true;

        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Miner"))
        {
            CanBreak = false;
        }
    }

    void SpawnPoints()
    {

        for (int i = 0; i < Mathf.FloorToInt(RockValueInPoints); i++)
        {
            Instantiate(pointsPrefab, transform.position, Quaternion.identity);
        }
        spawnedPoints = true;
    }
    void InstantiateVFX()
    {
        Vector3 currentPosition = transform.position;

        float offset = 1.5f;
        currentPosition.y += offset;

        Instantiate(brokenVfx, currentPosition, Quaternion.identity);
    }
}
