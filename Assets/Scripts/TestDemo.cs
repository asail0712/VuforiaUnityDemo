using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class TestDemo : MonoBehaviour
{
    [SerializeField] DefaultObserverEventHandler observerEventHandler;

	private Coroutine trackCoroutine = null;

	private void OnEnable()
	{
		if (observerEventHandler == null)
		{
			return;
		}

		observerEventHandler.OnTargetFound.AddListener(OnTargetFound);
		observerEventHandler.OnTargetLost.AddListener(OnTargetLost);
	}

	private void OnDisable()
	{
		OnTargetLost();

		if (observerEventHandler == null)
		{
			return;
		}

		observerEventHandler.OnTargetFound.RemoveListener(OnTargetFound);
		observerEventHandler.OnTargetLost.RemoveListener(OnTargetLost);
	}

	private void OnTargetFound()
	{

		ObserverBehaviour mObserverBehaviour = GetComponent<ObserverBehaviour>();

		if (mObserverBehaviour == null)
		{
			return;
		}

		string targetKey	= mObserverBehaviour.TargetName;
		GameObject go		= mObserverBehaviour.gameObject;

		if(trackCoroutine != null)
		{
			StopCoroutine(trackCoroutine);
		}

		trackCoroutine = StartCoroutine(TrackARModel(targetKey, go));
	}

	private void OnTargetLost()
	{
		if (trackCoroutine != null)
		{
			StopCoroutine(trackCoroutine);
		}
	}

	private IEnumerator TrackARModel(string targetKey, GameObject go)
	{

		while (true)
		{ 
			yield return new WaitForSeconds(1f);

			Debug.Log($"{targetKey} => {go.transform.position}");
		}
	}
}
