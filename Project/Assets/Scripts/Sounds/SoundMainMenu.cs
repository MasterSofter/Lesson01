using System.Collections;
using System.Collections.Generic;
using EventBus.Interfaces;
using UnityEngine;
using Zenject;

public class SoundMainMenu : MonoBehaviour, ISoundSource
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


	public void OnChangedState(EnumMenuState newState)
	{
		Debug.Log($"Сработало событие в SoundMenu {newState}");
		switch (newState)
		{
			case EnumMenuState.StartMenu:
				_played = false;
				Play();
				break;
			case EnumMenuState.Menu:
				if (!_played) Play();
				break;
		}
	}

	[Inject]
	public void Construct(IEventBus eventBus)
	{
		Debug.Log("Конструктор SoundMainMenu");
		_eventBus = eventBus;
		_eventBus.GetEvent<MenuDataModelChangedState<EnumMenuState>>().Subscribe(OnChangedState);
	}
}
