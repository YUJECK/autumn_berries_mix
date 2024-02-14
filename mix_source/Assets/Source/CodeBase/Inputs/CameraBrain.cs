using System;
using autumn_berries_mix.Grid.Inputs;
using Cinemachine;
using UnityEngine;

namespace autumn_berries_mix
{
    
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public sealed class CameraBrain : MonoBehaviour
    {
        private CinemachineVirtualCamera _virtualCamera;
        
        private void Start()
        {
            _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        }

        private void Update()
        {
            if (InputsHandler.CameraZoomEdit != 0)
            {
                float newLens = _virtualCamera.m_Lens.OrthographicSize + -1 * InputsHandler.CameraZoomEdit;

                newLens = Mathf.Clamp(newLens, 1, 40);

                _virtualCamera.m_Lens.OrthographicSize = newLens;
            }
                
            
            transform.position += new Vector3(InputsHandler.CameraMovement.x, InputsHandler.CameraMovement.y, 0) * (Time.deltaTime * 10);
        }
    }
}
