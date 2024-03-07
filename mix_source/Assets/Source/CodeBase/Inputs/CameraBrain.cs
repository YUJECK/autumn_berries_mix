using System;
using autumn_berries_mix.Grid.Inputs;
using Cinemachine;
using Cysharp.Threading.Tasks;
using Unity.Collections;
using UnityEngine;
using Random = System.Random;

namespace autumn_berries_mix
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public sealed class CameraBrain : MonoBehaviour
    {
        private CinemachineVirtualCamera _virtualCamera;
        private bool CurrentZooming => InputsHandler.CameraZoomEdit != 0;
        
        private void Start()
        {
            _virtualCamera = GetComponent<CinemachineVirtualCamera>();
            CameraFloating();
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

        private async void CameraFloating()
        {
            float a;
            float start;

            bool edited = false;
            
            Regenerate();
            
            while (gameObject.activeSelf)
            {
                while (!CurrentZooming)
                {
                    _virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(_virtualCamera.m_Lens.OrthographicSize, _virtualCamera.m_Lens.OrthographicSize + a, Time.deltaTime);
                    
                    if(Mathf.Abs(_virtualCamera.m_Lens.OrthographicSize - start) > Mathf.Abs(a))
                        break;
                    
                    edited = true;
                    await UniTask.WaitForFixedUpdate();
                }

                if (edited)
                {
                    Regenerate();
                    edited = false;
                }
                
                await UniTask.WaitForFixedUpdate();
            }

            void Regenerate()
            {
                a = UnityEngine.Random.Range(-0.15f, 0.15f);
                start = _virtualCamera.m_Lens.OrthographicSize;
            }
        }
        
    }
}
