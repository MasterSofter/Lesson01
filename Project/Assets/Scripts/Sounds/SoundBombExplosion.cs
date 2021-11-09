using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBombExplosion : MonoBehaviour, ISoundSource
{

	[SerializeField] private AudioSource _source;
	[SerializeField] private AudioClip _audioClip;
	[SerializeField] private float _volume = 1f;
	[SerializeField] private bool _loop = false;

	public void Play()
	{
		if (_source != null)
		{
			_source.clip = _audioClip;
			_source.volume = _volume;
			_source.loop = _loop;

			_source.Play();
		}

	}

	public bool IsPlaying() => _source.isPlaying;
	public void Stop() => _source.Stop();

	private void Start() => Play();
}
