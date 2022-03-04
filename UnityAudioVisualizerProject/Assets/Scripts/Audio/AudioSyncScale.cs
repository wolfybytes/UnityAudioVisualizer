using System.Collections;
using UnityEngine;

public class AudioSyncScale : AudioSyncer
{
    public Vector3 beatScale;
    public Vector3 restScale;

    private IEnumerator MoveToScale(Vector3 _targetScale)
    {
        Vector3 _currentScale = transform.localScale;
        Vector3 _initialScale = _currentScale;
        float _timer = 0;

        while (_currentScale != _targetScale)
        {
            _currentScale = Vector3.Lerp(_initialScale, _targetScale, _timer / timeToBeat);
            _timer += Time.deltaTime;

            transform.localScale = _currentScale;

            yield return null;
        }
        m_isBeat = false;
    }

    public override void OnBeat()
    {
        base.OnBeat();

        StopCoroutine("MoveToScale");
        StartCoroutine("MoveToScale", beatScale);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (m_isBeat)
            return;

        transform.localScale = Vector3.Lerp(transform.localScale, restScale, restLerpTime * Time.deltaTime);
    }
}
