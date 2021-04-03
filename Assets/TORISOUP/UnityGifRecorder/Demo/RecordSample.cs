using System;
using System.IO;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace TORISOUP.UnityGifRecorder.Demo
{
    public class RecordSample : MonoBehaviour
    {
        [SerializeField] private UnityGifRecorder _recorder;

        private async UniTaskVoid Start()
        {
            var ct = this.GetCancellationTokenOnDestroy();

            var recordingSeconds = 3.0f;

            _recorder.Setup(
                autoAspect: true,
                width: 600,
                height: 300,
                fps: 15,
                bufferSize: recordingSeconds + 1.0f,  // Need to longer than the recording time.
                repeat: 0,
                quality: 50
            );

            _recorder.SaveFolder = Path.Combine(Application.dataPath, "TORISOUP", "UnityGifRecorder", "Demo", "output");

            await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: ct);

            Debug.Log("Start recording!");

            await _recorder.RecordAsync(seconds: recordingSeconds, DelayType.DeltaTime, token: ct);

            Debug.Log("Recording complete.");

            Debug.Log("Saving...");

            await _recorder.SaveAsync(fileName: "TestGifRecord");

            Debug.Log("Done!");
        }
    }
}