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
        if (CanBreak == true && Input.GetKeyDown(KeyCode.F) && Change.IsMiner == true)
        {
            if (!spawnedPoints) SpawnPoints();
            AudioManager.Instance.PlaySFX("DestroyRock");
            Destroy(Rock);
            Instantiate(brokenVfx, transform.position, Quaternion.identity);
         
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
}
