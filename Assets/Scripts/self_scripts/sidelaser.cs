using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sidelaser : MonoBehaviour
{
    [SerializeField] private LineRenderer _beam;
    [SerializeField] private Transform _muzzlePoint;
    [SerializeField] private float _maxLength;
    [SerializeField] private float _damage;
    [SerializeField] private AudioSource _soundEffect;

    private Vector3 hitPosition;

    private void Awake()
    {
        _beam.enabled = false;
    }

    private void Activate()
    {
        _beam.enabled = true;
        _soundEffect.Play();
    }

    private void Deactivate()
    {
        _beam.enabled = false;
        _beam.SetPosition(0, _muzzlePoint.position);
        _beam.SetPosition(1, _muzzlePoint.position);
        _soundEffect.Stop();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            Activate();
        else if (Input.GetKeyUp(KeyCode.Mouse0))
            Deactivate();
    }

    private void FixedUpdate()
    {
        if (!_beam.enabled) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit target))
        {
            if (target.transform.TryGetComponent<MineHealth>(out MineHealth minehealth))
            {
                minehealth.DecreaseHealth(_damage * Time.fixedDeltaTime);
            }
            _beam.SetPosition(0, _muzzlePoint.position);
            _beam.SetPosition(1, target.point);
        }
    }
}

