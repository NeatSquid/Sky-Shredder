using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private GameControls _controls;

    [Header("Settings")] [Space] [SerializeField] [Range(0f, 100f)]
    private float _controlSpeed = 10f;

    [SerializeField] [Range(0f, 20f)] private float _horizontalRange = 12f;
    [SerializeField] [Range(0f, 20f)] private float _verticalRange = 8f;

    private Vector2 _moveInput;
    [SerializeField] private Transform _rotateMe;

    // [SerializeField] private Transform _center;
    [SerializeField] private float _positionPitchFactor = 5f;
    [SerializeField] private float _ctrlPitchFactor = 4f;
    [SerializeField] private float _positionYawFactor = 4f;
    [SerializeField] private float _ctrlRollFactor = 4f;

    [SerializeField] private ParticleSystem[] _laserBeams;

    private void Awake()
    {
        _controls = new GameControls();
        _controls.Enable();

        StopBeams();
        _controls.Gameplay.Fire.started += _ =>
        {
            foreach (var beam in _laserBeams)
                beam.Play();
        };
        _controls.Gameplay.Fire.canceled += _ => { StopBeams(); };
    }

    private void StopBeams()
    {
        foreach (var beam in _laserBeams)
            beam.Stop();
    }

    private void OnDestroy()
    {
        _controls.Disable();
    }

    private void Update()
    {
        _moveInput = _controls.Gameplay.Movement.ReadValue<Vector2>();

        ProcessTranslation();
        ProcessRotation();
    }

    private void ProcessRotation()
    {
        var localPosition = transform.localPosition;

        var pitch = localPosition.y * -_positionPitchFactor + _moveInput.y * -_ctrlPitchFactor;
        var yaw = localPosition.x * -_positionYawFactor;
        var roll = _moveInput.x * _ctrlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, 0f);
        _rotateMe.localRotation = Quaternion.Euler(0f, 0f, roll);
    }

    private void ProcessTranslation()
    {
        var tran = transform;
        var pos = tran.localPosition;

        tran.Translate(_moveInput * (_controlSpeed * Time.deltaTime), Space.Self);

        var newPos = transform.localPosition;

        newPos.x = Mathf.Clamp(newPos.x, -_horizontalRange, _horizontalRange);
        newPos.y = Mathf.Clamp(newPos.y, -_verticalRange, _verticalRange);

        tran.localPosition = new Vector3(newPos.x, newPos.y, pos.z);
    }
}