using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpellSelector : MonoBehaviour
{
    public static Action<SpellTypes> SpellSelected;

    [SerializeField] private Material magicMissile;
    [SerializeField] private Material Fire;
    [SerializeField] private Material Lightning;
    [SerializeField] private Material Earth;

    [SerializeField] private InputActionReference moveActionReference;
    [SerializeField] private GameObject book;
    private MeshRenderer bookMR;

    private SpellTypes selectedSpell = SpellTypes.Fire;
    private Vector2 joystickAxisValue;

    private bool debounce = false;

    // Start is called before the first frame update
    void Start()
    {
        ChangeHandColour(false);
        bookMR = book.GetComponent<MeshRenderer>(); // Return null for some fucking reason
    }

    // Update is called once per frame
    void Update()
    {
        joystickAxisValue = moveActionReference.action.ReadValue<Vector2>();

        if (debounce == false)
        {
            // Right
            if (joystickAxisValue.x > 0.75f)
            {
                debounce = true;
                ChangeHandColour(true);
            }

            // Left
            else if (joystickAxisValue.x < -0.75f)
            {
                debounce = true;
                ChangeHandColour(false);
            }
        }

        if (joystickAxisValue.x < 0.75f && joystickAxisValue.x > -0.75f)
        {
            debounce = false;
        }
    }

    void ChangeHandColour(bool nextSpell)
    {
        int enumSize = Enum.GetNames(typeof(SpellTypes)).Length;

        if (nextSpell)
        {
            if ((int)selectedSpell + 1 < enumSize)
            {
                selectedSpell = (SpellTypes)((int)selectedSpell + 1);
            }
        }
        else
        {
            if ((int)selectedSpell - 1 >= 0)
            {
                selectedSpell = (SpellTypes)((int)selectedSpell - 1);
            }
        }

        SetBookMaterial(selectedSpell);
        FireSpellSelectedEvent();
    }

    void SetBookMaterial(SpellTypes type)
    {
        // This makes no sense, with this GetComponent bookMR is null even though it should be initialized in Start()
        bookMR = book.GetComponent<MeshRenderer>();

        switch (type)
        {
            case SpellTypes.Gun:
                if (bookMR.material == null)
                {
                    Debug.Log("bruh");
                }
                if (magicMissile == null)
                {
                    Debug.Log("even more bruh");
                }
                bookMR.material = magicMissile;
                break;

            case SpellTypes.Fire:
                bookMR.material = Fire;
                break;

            case SpellTypes.Lightning:
                bookMR.material =  Lightning;
                break;

            case SpellTypes.Earth:
                bookMR.material =  Earth;
                break;

            default:
                bookMR.material =  magicMissile;
                break;
        }
    }

    void FireSpellSelectedEvent()
    {
        SpellSelected?.Invoke(selectedSpell);
    }
}
