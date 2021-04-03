using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Moments;
using UnityEngine;

namespace TORISOUP.UnityGifRecorder
{
    [RequireComponent(typeof(Recorder))]
    [DisallowMultipleComponent]
    public sealed class UnityGifRecorder : MonoBehaviour
    {
        private Recorder _recorder;

        private void Awake()
        {
            _recorder = GetComponent<Recorder>();
        }

        public string SaveFolder
        {
            get => _recorder.SaveFolder;
            set => _recorder.SaveFolder = value;
        }

        public RecorderState State => _recorder.State;
        
        public void Setup(bool autoAspect, int width, int height, int fps, float bufferSize, int repeat, int quality)
        {
            _recorder.Setup(autoAspect, width, height, fps, bufferSize, repeat, quality);
        }

        public async UniTask RecordAsync(
            float seconds,
            DelayType delayType = DelayType.DeltaTime,
            CancellationToken token = default)
        {
            try
            {
                await UniTask.WaitWhile(() => _recorder.State == RecorderState.PreProcessing, cancellationToken: token);

                _recorder.Record();

                await UniTask.Delay(TimeSpan.FromSeconds(seconds), delayType, cancellationToken: token);

                _recorder.Pause();
            }
            catch (OperationCanceledException)
            {
                if (_recorder.State == RecorderState.Recording)
                {
                    _recorder.Pause();
                    _recorder.FlushMemory();
                }

                throw;
            }
        }

        public async UniTask SaveAsync(string fileName)
        {
            var utc = AutoResetUniTaskCompletionSource.Create();

            _recorder.Save(fileName);
            _recorder.OnFileSaved += (_, __) =>
            {
                utc.TrySetResult();
                _recorder.OnFileSaved = null;
            };

            await utc.Task;
        }

        public void Pause()
        {
            _recorder.Pause();
        }
    }
}