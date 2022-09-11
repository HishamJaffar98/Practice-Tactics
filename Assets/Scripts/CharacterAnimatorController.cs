using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimatorController : MonoBehaviour
{

	[SerializeField] private Animator CharacterAnimator;
	private void OnEnable()
	{
		Movement.OnCharacterMoving += PlayCharacterMoveAnimation;
		Movement.OnCharacterIdle += PlayCharacterIdleAnimation;
	}

	private void OnDisable()
	{
		Movement.OnCharacterMoving -= PlayCharacterMoveAnimation;
		Movement.OnCharacterIdle -= PlayCharacterIdleAnimation;
	}
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void PlayCharacterMoveAnimation()
	{
		CharacterAnimator.SetBool(AnimationTags.isMoving, true);
	}
	private void PlayCharacterIdleAnimation()
	{
		CharacterAnimator.SetBool(AnimationTags.isMoving, false);
	}

}
