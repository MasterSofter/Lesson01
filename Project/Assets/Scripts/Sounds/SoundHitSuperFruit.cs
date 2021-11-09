using System.Collections;
using System.Collections.Generic;
using EventBus.Interfaces;
using UnityEngine;
using Zenject;

public class SoundHitSuperFruit : MonoBehaviour, ISoundSource
{
	public IEventBus _eventBus;
	[SerializeField] private AudioSource _source;
	[SerializeField] private AudioClip[] _audioClips;
	[SerializeField] private float _volume = 1f;
	[SerializeField] private bool _loop = false;

	public void Play()
	{
		if (_source != null)
		{
			_source.clip = _audioClips[Random.RandomRange(0, _audioClips.Length - 1)];
			_source.volume = _volume;
			_source.loop = _loop;

			_source.Play();
		}

	}

	public void OnHitFruit(SuperFruitVm superFruitVm) => Play();

	public bool IsPlaying() => _source.isPlaying;
	public void Stop() => _source.Stop();

	[Inject]
    public void Init(IEventBus eventBus)
    {
		_eventBus = eventBus;
		_eventBus.GetEvent<FruitCutEvent<SuperFruitVm>>().Subscribe(OnHitFruit);
    }
}
