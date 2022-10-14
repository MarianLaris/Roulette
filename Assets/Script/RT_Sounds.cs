using UnityEngine;
using System.Collections;

public class RT_Sounds : MonoBehaviour 
{

	public AudioClip _ButtonClick;
	public AudioClip _NotEnoughAmount;
	public AudioClip _Win;
	public AudioClip _Loss;
	public AudioClip _RoulettRotation;
	public AudioClip _Spin;
	public AudioClip _Clear;
	public AudioClip _CoinSelected;
	public AudioClip _CoinMissed;
	public AudioClip _CoinPlaced;
	public AudioClip _AddAmount;
	public AudioClip _CollectNow;
	public AudioClip _WinningNo;
	public AudioClip _GameSound;
	public AudioClip _BigWin;
	public AudioClip _AvgWin;
	public AudioClip _SpinSound;
	
	public void PlaySound(AudioClip isAudioClip)
	{
		audio.clip=isAudioClip;
		audio.Play();
	}
}

