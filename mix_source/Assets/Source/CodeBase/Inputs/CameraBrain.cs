using System;
using autumn_berries_mix.CallbackSystem.Signals;
using autumn_berries_mix.Gameplay.Signals;
using autumn_berries_mix.Grid.Inputs;
using Cinemachine;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace autumn_berries_mix
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public sealed class CameraBrain : MonoBehaviour
    {
        public Transform[] particlesContainer;
        public Transform cameraCenter;
        public Transform cursor;
        
        private CinemachineVirtualCamera _virtualCamera;
        private bool CurrentZooming => InputsHandler.CameraZoomEdit != 0;
        
        private void Start()
        {
            _virtualCamera = GetComponent<CinemachineVirtualCamera>();

            SignalManager.SubscribeOnSignal<UnitDamagedSignal>(OnUnitHit);
            SignalManager.SubscribeOnSignal<SomethingBroken>(OnSomethingBroken);
        }

        private void OnSomethingBroken(SomethingBroken obj)
        {
            Shake(1.3f, 0.1f);
        }

        private void OnUnitHit(UnitDamagedSignal unit)
        {
            Shake(1, 0.1f);
        }

        private void Update()
        {
            if (InputsHandler.CameraZoomEdit != 0)
            {
                float newLens = _virtualCamera.m_Lens.OrthographicSize + -1 * InputsHandler.CameraZoomEdit;

                newLens = Mathf.Clamp(newLens, 1, 40);

                foreach (var particle in particlesContainer)
                {
                    particle.transform.localScale *= newLens/_virtualCamera.m_Lens.OrthographicSize;    
                }
                
                _virtualCamera.m_Lens.OrthographicSize = newLens;
            }

            cameraCenter.position += new Vector3(InputsHandler.CameraMovement.x, InputsHandler.CameraMovement.y, 0) * (Time.deltaTime * 10);
            cursor.position = InputsHandler.GetMousePosition;
        }

        public async void Shake(float intensity, float time)
        {
            var shake = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            shake.m_AmplitudeGain = intensity;

            await UniTask.Delay(TimeSpan.FromSeconds(time));
            
            shake.m_AmplitudeGain = 0;
        }
    }
}
