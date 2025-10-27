using R3;
using UnityEngine;

public class TestR3 : MonoBehaviour
{
    private CompositeDisposable d = new();

    void Start()
    {
        Observable.EveryUpdate()
            .Subscribe(_ => transform.Rotate(0, 90 * Time.deltaTime, 0))
            .AddTo(d);
    }

    void OnDestroy() => d.Dispose();
}