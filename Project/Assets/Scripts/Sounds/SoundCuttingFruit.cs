using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCuttingFruit : MonoBehaviour,ISoundSource
{

	[SerializeField] private AudioSource _source;
	[SerializeField] private AudioClip[] _audioClips;
	[SerializeField] private float _volume = 1f;
	[SerializeField] private bool _loop = false;

	public void Play()
	{
		if (_source != null) {
			_source.clip = _audioClips[Random.RandomRange(0, _audioClips.Length - 1)];
			_source.volume = _volume;
			_source.loop = _loop;

			_source.Play();
		}
	
	}

	public bool IsPlaying() => _source.isPlaying;
	public void Stop() => _source.Stop();

    private void Start() => Play();


}
