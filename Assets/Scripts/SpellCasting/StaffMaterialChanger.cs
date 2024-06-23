using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StaffMaterialChanger : MonoBehaviour
{
    [SerializeField] private Material baseMaterial;
    [SerializeField] private Material fireMaterial;
    [SerializeField] private Material lightningMaterial;
    [SerializeField] private Material earthMaterial;

    private MeshRenderer mr;

    private void OnEnable()
    {
        CastDetector.OnCast += OnCastEventHandler;
    }

    private void OnDisable()
    {
        CastDetector.OnCast -= OnCastEventHandler;
    }

    // Start is called before the first frame update
    void Start()
    {
        mr = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCastEventHandler(SpellTypes type)
    {
        switch (type)
        {
            case SpellTypes.Gun:
                mr.material = baseMaterial;
                break;

            case SpellTypes.Fire:
                mr.material = fireMaterial;
                break;

            case SpellTypes.Lightning:
                mr.material = lightningMaterial;
                break;

            case SpellTypes.Earth:
                mr.material = earthMaterial;
                break;

            default:
                mr.material = baseMaterial;
                break;
        }
    }
}
