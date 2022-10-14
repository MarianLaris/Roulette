using UnityEngine;
using System.Collections;


[System.Serializable]
public class AnimationParam
{
	public float _Delay=1.0f;
	public float _Duration=1.0f;
	public int _RepeatCount=0;
	public eLoopType _LoopType=eLoopType.Normal;
	public bool _PingPog=false;
	public _EaseType _easeType=_EaseType.linear;
	public eAnimationSequence _AnimationPriority;
}

public class MenuItemEntryAnimation : MonoBehaviour {
	
	// Use this for initialization
	public delegate void callback();
	callback mcallback;
	public Vector3 _StartPosition;
	public Vector3 _StartScale;
	public Vector3 _StartRotation;
	
	public Vector3 _EndPosition;
	public Vector3 _EndScale;
	public Vector3 _EndRotation;

	public bool _SequenceAnimation = false;
	public bool _SpawnAnimation = false;
	
	public eSpawnCombination _SpawnCombination;
	public eSequenceCombination _SequenceCombination;
	
	public AnimationParam _Trans=new AnimationParam();
	public AnimationParam _Rotate=new AnimationParam();
	public AnimationParam _Scale=new AnimationParam();
	public AnimationParam _Spawn=new AnimationParam();
	
	void Start () 
	{
		Camera.main.orthographicSize=Screen.height/200f;

		//StartSpawnAnimation(_SpawnCombination);
		StartSequenceAnimation(_SequenceCombination);
	}

	void StartSequenceAnimation(eSequenceCombination comb)
	{
		switch(comb)
		{
			case eSequenceCombination.T:
				 Move(_Trans,fun=>AnimationComplete());
			break;
			case eSequenceCombination.R:
				 Rotate(_Rotate,fun=>AnimationComplete());
			break;
			case eSequenceCombination.S:
				 Scale(_Scale,fun=>AnimationComplete());
			break;
			case eSequenceCombination.TR:
				 Move(_Trans,fun=>Rotate(_Rotate,fun1=>AnimationComplete()));
			break;
			case eSequenceCombination.RT:
				 Rotate(_Rotate,fun=>Move(_Trans,fun1=>AnimationComplete()));
			break;
			case eSequenceCombination.RS:
				 Rotate(_Rotate,fun=>Scale(_Scale,fun1=>AnimationComplete()));
			break;
			case eSequenceCombination.SR:
				 Scale(_Scale,fun=>Rotate(_Rotate,fun1=>AnimationComplete()));
			break;
			case eSequenceCombination.TS:
				 Move(_Trans,fun=>Scale(_Scale,fun1=>AnimationComplete()));
			break;
			case eSequenceCombination.ST:
				 Scale(_Scale,fun=>Move(_Trans,fun1=>AnimationComplete()));
			break;
			case eSequenceCombination.TRS:
				Move(_Trans,fun=>Rotate(_Rotate,fun1=>Scale(_Scale,fun2=>AnimationComplete())));
			break;
			case eSequenceCombination.TSR:
				 Move(_Trans,fun=>Scale(_Scale,fun1=>Rotate(_Rotate,fun2=>AnimationComplete())));
			break;
			case eSequenceCombination.RTS:
				 Rotate(_Rotate,fun=>Move(_Trans,fun1=>Scale(_Scale,fun2=>AnimationComplete())));
			break;
			case eSequenceCombination.RST:
				 Rotate(_Rotate,fun=>Scale(_Scale,fun1=>Move(_Trans,fun2=>AnimationComplete())));
			break;
			case eSequenceCombination.SRT:
				 Scale(_Scale,fun=>Rotate(_Rotate,fun1=>Move(_Trans,fun2=>AnimationComplete())));
			break;
			case eSequenceCombination.STR:
				 Scale(_Scale,fun=>Move(_Trans,fun1=>Rotate(_Rotate,fun2=>AnimationComplete())));
			break;
		}
	}
	
	void StartSpawnAnimation(eSpawnCombination comb)
	{
		switch(comb)
		{
			case eSpawnCombination.TRS:
				Move(_Spawn,null);
				Rotate(_Spawn,null);
				Scale(_Spawn,fun=>AnimationComplete());
			break;
			case eSpawnCombination.RS:
				Rotate(_Spawn,null);
				Scale(_Spawn,fun=>AnimationComplete());
			break;
			case eSpawnCombination.TR:
				Move(_Spawn,null);
				Rotate(_Spawn,fun=>AnimationComplete());
			break;
			case eSpawnCombination.TS:
				Move(_Spawn,null);
				Scale(_Spawn,fun=>AnimationComplete());
			break;
		}
	}
	
	void Move(AnimationParam anim,RTS.CallBack call)
	{
		Animations.Move(gameObject,_StartPosition,_EndPosition,anim._Duration,anim._Delay,anim._easeType,anim._RepeatCount,false,anim._PingPog,call);
	}
	void Scale(AnimationParam anim,RTS.CallBack call)
	{
		Animations.Scale(gameObject,_StartScale,_EndScale,anim._Duration,anim._Delay,anim._easeType,anim._RepeatCount,false,anim._PingPog,call);
	}
	void Rotate(AnimationParam anim,RTS.CallBack call)
	{
		Animations.Rotate(gameObject,_StartRotation,_EndRotation,anim._Duration,anim._Delay,anim._easeType,anim._RepeatCount,false,anim._PingPog,call);
	}
	
	void AnimationComplete()
	{
		Debug.Log("Entry Enimation Complete....");
	}
}
