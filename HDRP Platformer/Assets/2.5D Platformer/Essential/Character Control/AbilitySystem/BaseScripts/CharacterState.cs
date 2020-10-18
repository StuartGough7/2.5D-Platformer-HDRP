﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class CharacterState : StateMachineBehaviour
    {
        public CharacterControl control;

        [Space(10)]
        public List<CharacterAbility> ListAbilityData = new List<CharacterAbility>();
        [Space(10)]
        public CharacterAbility[] ArrAbilities;

        [Space(20)]
        [Header("Character Ability Types")]
        public CharacterAbility[] ArrMove;
        [Space(10)]
        public CharacterAbility[] ArrTransition;
        [Space(10)]
        public CharacterAbility[] ArrAttack;
        [Space(10)]
        public CharacterAbility[] ArrOther;

        public GameObject RIGHT_HAND_ATTACK => control.characterSetup.attackPartSetup.RightHand_Attack;

        //temp
        public MomentumData MOMENTUM_DATA => control.characterData.momentumData;
        public RotationData ROTATION_DATA => control.characterData.rotationData;
        public JumpData JUMP_DATA => control.characterData.jumpData;
        public CollisionSphereData COLLISION_SPHERE_DATA => control.characterData.collisionSphereData;
        public GroundData GROUND_DATA => control.characterData.groundData;
        public AttackData ATTACK_DATA => control.characterData.attackData;
        public AnimationData ANIMATION_DATA => control.characterData.animationData;
        public AIController AI_CONTROLLER => control.aiController;
        public WeaponData WEAPON_DATA => control.characterData.weaponData;
        public CameraData CAMERA_DATA => control.characterData.cameraData;
        public TurnData TURN_DATA => control.characterData.turnData;

        public void PutStatesInArray()
        {
            ArrAbilities = new CharacterAbility[ListAbilityData.Count];
            
            for(int i = 0; i < ListAbilityData.Count; i++)
            {
                ArrAbilities[i] = ListAbilityData[i];
            }
        }

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            EnterAll(this, animator, stateInfo, ArrAbilities);

            EnterAll(this, animator, stateInfo, ArrMove);
            EnterAll(this, animator, stateInfo, ArrTransition);
            EnterAll(this, animator, stateInfo, ArrAttack);
            EnterAll(this, animator, stateInfo, ArrOther);
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            UpdateAll(this, animator, stateInfo, ArrAbilities);

            UpdateAll(this, animator, stateInfo, ArrMove);
            UpdateAll(this, animator, stateInfo, ArrTransition);
            UpdateAll(this, animator, stateInfo, ArrAttack);
            UpdateAll(this, animator, stateInfo, ArrOther);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            ExitAll(this, animator, stateInfo, ArrAbilities);

            ExitAll(this, animator, stateInfo, ArrMove);
            ExitAll(this, animator, stateInfo, ArrTransition);
            ExitAll(this, animator, stateInfo, ArrAttack);
            ExitAll(this, animator, stateInfo, ArrOther);
        }

        public void EnterAll(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo, CharacterAbility[] AbilityList)
        {
            for (int i = 0; i < AbilityList.Length; i++)
            {
                AbilityList[i].OnEnter(characterState, animator, stateInfo);

                if (control.ANIMATION_DATA.CurrentRunningAbilities.ContainsKey(AbilityList[i]))
                {
                    control.ANIMATION_DATA.CurrentRunningAbilities[AbilityList[i]] += 1;
                }
                else
                {
                    control.ANIMATION_DATA.CurrentRunningAbilities.Add(AbilityList[i], 1);
                }
            }
        }

        public void UpdateAll(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo, CharacterAbility[] AbilityList)
        {
            for (int i = 0; i < AbilityList.Length; i++)
            {
                AbilityList[i].UpdateAbility(characterState, animator, stateInfo);
            }
        }

        public void ExitAll(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo, CharacterAbility[] AbilityList)
        {
            for (int i = 0; i < AbilityList.Length; i++)
            {
                AbilityList[i].OnExit(characterState, animator, stateInfo);

                if (control.ANIMATION_DATA.CurrentRunningAbilities.ContainsKey(AbilityList[i]))
                {
                    control.ANIMATION_DATA.CurrentRunningAbilities[AbilityList[i]] -= 1;

                    if (control.ANIMATION_DATA.CurrentRunningAbilities[AbilityList[i]] <= 0)
                    {
                        control.ANIMATION_DATA.CurrentRunningAbilities.Remove(AbilityList[i]);
                    }
                }
            }
        }

        public bool UpdatingAbility(System.Type characterAbilityType)
        {
            return control.GetBool(typeof(CurrentAbility), characterAbilityType);
        }
    }
}