using System.Collections;
using System.Collections.Generic;
using EventBus.Interfaces;
using UnityEngine;
using Zenject;

public class SoundGameOver : MonoBehaviour, ISoundSource
{
	private IEventBus _eventBus;
	private bool _played = false;

	[SerializeField] private AudioSource _source;
	[SerializeField] private AudioClip _audioClip;
	[SerializeField] private float _volume = 1f;
	[SerializeField] private bool _loop = false;


	public void Play()
	{
		_source.clip = _audioClip;
		_source.volume = _volume;
		_source.loop = _loop;

		_source.Play();
		_played = true;
	}

	public bool IsPlaying() => _source.isPlaying;
	public void Stop() => _source.Stop();


	public void OnChangedState(EnumGameState newState)
	{
		switch (newState)
		{
			case EnumGameState.GameOver:
				if (!_played) Play();
				break;
			case EnumGameState.StartGame:
				_played = false;
				break;
		}
	}

	[Inject]
	public void Construct(IEventBus eventBus)
	{
		_eventBus = eventBus;
		_eventBus.GetEvent<GameDataModelChangedEvent<EnumGameState>>().Subscribe(OnChangedState);
	}
}
