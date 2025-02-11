﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/CharacterAbilities/WallSlide")]
    public class WallSlide : CharacterAbility
    {
        public Vector3 MaxFallVelocity;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterState.control.MoveLeft = false;
            characterState.control.MoveRight = false;

            characterState.DATASET.MOVE_DATA.Momentum = 0f;
            characterState.DATASET.JUMP_DATA.CanWallJump = false;
            characterState.DATASET.VERTICAL_VELOCITY_DATA.MaxWallSlideVelocity = MaxFallVelocity;
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (!characterState.control.Jump)
            {
                characterState.DATASET.JUMP_DATA.CanWallJump = true;
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterState.DATASET.VERTICAL_VELOCITY_DATA.MaxWallSlideVelocity = Vector3.zero;
        }
    }
}