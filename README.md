# UnityGifRecorder

This is a library for shooting Gif videos in Unity.  
This library modifies [Moments](https://github.com/Chman/Moments) to be compatible with the latest version of Unity and adds an asynchronous API.


![Gif](Assets\TORISOUP\UnityGifRecorder\Demo\output\TestGifRecord.gif)


Depends on  [Moments](https://github.com/Chman/Moments) and [UniTask](https://github.com/Cysharp/UniTask).

# How to use

```cs
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

        Debug.Log("Start!");

        await _recorder.RecordAsync(seconds: recordingSeconds, DelayType.DeltaTime, token: ct);

        Debug.Log("Saving...");

        await _recorder.SaveAsync(fileName: "TestGifRecord");

        Debug.Log("Done!");
    }
}
```

# LICENSE

MIT License.

# Rights

## Moments

[Zlib license](https://github.com/Chman/Moments/blob/master/LICENSE.txt)

## UniTask

The MIT License (MIT) Copyright (c) 2019 Yoshifumi Kawai / Cysharp, Inc.

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.