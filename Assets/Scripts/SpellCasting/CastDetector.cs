using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CastDetector : MonoBehaviour
{
    public static Action<SpellTypes> OnCast;

    [SerializeField] private SpellTypes type;

    [Space(10)]
    [Header("Audio Settings")]
    [SerializeField] private AudioClip orbHitClip;
    [SerializeField] private float beginPitch;
    [SerializeField] private float endPitch;

    [Header("Haptic Feedback Settings")]
    [SerializeField] private XRBaseController controller;
    [Range(0, 1)]
    [SerializeField] private float intensity;
    [SerializeField] private float duration;

    [Space(10)]
    [SerializeField] private GameObject parent;
    [SerializeField] private GameObject particle;
    [SerializeField] private List<Material> pointMaterials;

    [Space(10)]
    [SerializeField] private List<GameObject> points;

    private int currentPoint;

    private void OnEnable()
    {
        SpellSelector.SpellSelected += SpellSelectedEventHandler;
        StaffOrbDetector.OnCollision += StaffOnCollisionEventHandler;
    }

    private void OnDisable()
    {
        SpellSelector.SpellSelected -= SpellSelectedEventHandler;
        StaffOrbDetector.OnCollision -= StaffOnCollisionEventHandler;
    }

    void SpellSelectedEventHandler(SpellTypes selectedSpell)
    {
        if (selectedSpell == type)
        {
            if (points.Count == 0) FireOnCastEvent(type);
            parent.SetActive(true);

            UpdateAllPointMaterials();
        }
        else
        {
            parent.SetActive(false);

            currentPoint = 0;
            foreach (GameObject obj in points)
            {
                obj.SetActive(true);
            }
        }
    }

    void StaffOnCollisionEventHandler(List<GameObject> other)
    {
        if (points.Count > 0 && currentPoint < points.Count)
        {
            foreach (GameObject point in other.ToArray())
            {
                if (point == points[currentPoint])
                {
                    CreateAndPlayAudio();
                    currentPoint++;
                    point.SetActive(false);
                    Instantiate(particle, point.transform.position, Quaternion.identity);
                    UpdateAllPointMaterials();
                    TriggerHaptic();
                    other.Remove(point);

                    if (currentPoint < points.Count)
                    {
                        // Recursive
                        StaffOnCollisionEventHandler(other);
                    }
                    else
                    {
                        FireOnCastEvent(type);
                    }
                }
            }
        }

        /*if (points.Count > 0)
        {
            if (other == points[currentPoint])
            {
                other.gameObject.SetActive(false);
                if (currentPoint < points.Count - 1)
                {
                    currentPoint++;
                    UpdateAllPointMaterials();
                }
                else
                {
                    FireOnCastEvent(type);
                }
            }
        }*/
    }

    void FireOnCastEvent(SpellTypes type)
    {
        OnCast?.Invoke(type);
    }

    void UpdateAllPointMaterials()
    {
        for (int i = 0; i + currentPoint < points.Count; i++)
        {
            MeshRenderer mr = points[i + currentPoint].GetComponent<MeshRenderer>();
            if (i < pointMaterials.Count)
            {
                mr.material = pointMaterials[i];
            }
            else
            {
                mr.material = pointMaterials.Last();
            }
        }
    }

    void CreateAndPlayAudio()
    {
        GameObject obj = new GameObject("Orb Hit SFX");
        AudioSource src = obj.AddComponent<AudioSource>();

        src.clip = orbHitClip;
        src.loop = false;
        src.spatialBlend = 1f;
        src.volume = 2f;
        src.pitch = Mathf.Lerp(beginPitch, endPitch, (float)(currentPoint) / (float)(points.Count - 1));
        src.Play();

        Destroy(obj, orbHitClip.length + 1f);
    }

    void TriggerHaptic()
    {
        if (intensity > 0)
            controller.SendHapticImpulse(intensity, duration);
    }
}
