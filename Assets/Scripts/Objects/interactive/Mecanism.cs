using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mecanism : MonoBehaviour
{
    protected Renderer render;
    protected ChangeCharacter changeCharacter;
    [SerializeField]
    protected List<GameObject> scriptToActive = new List<GameObject>();

    public void Awake()
    {
        render = GetComponent<Renderer>();
        changeCharacter = FindObjectOfType<ChangeCharacter>();
    }
    public abstract void UseFunction(bool active);

}
