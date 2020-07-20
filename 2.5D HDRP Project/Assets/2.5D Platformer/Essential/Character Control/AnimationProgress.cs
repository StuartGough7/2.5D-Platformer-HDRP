﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class AnimationProgress : MonoBehaviour
    {
        private CharacterControl control;

        private void Awake()
        {
            control = GetComponent<CharacterControl>();
        }

        public void NullifyUpVelocity()
        {
            control.RIGID_BODY.velocity = new Vector3(
                            control.RIGID_BODY.velocity.x,
                            0f,
                            control.RIGID_BODY.velocity.z);
        }

        public bool ForwardIsReversed()
        {
            if (control.ANIMATION_DATA.LatestMoveForward.MoveOnHit)
            {
                if (control.GetBool(typeof(FacingAttacker)))// IsFacingAttacker())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            if (control.ANIMATION_DATA.LatestMoveForward.Speed > 0f)
            {
                return false;
            }
            else if (control.ANIMATION_DATA.LatestMoveForward.Speed < 0f)
            {
                return true;
            }

            return false;
        }

        public bool StateNameContains(string str)
        {
            AnimatorClipInfo[] arr = control.SkinnedMeshAnimator.GetCurrentAnimatorClipInfo(0);

            foreach(AnimatorClipInfo clipInfo in arr)
            {
                if (clipInfo.clip.name.Contains(str))
                {
                    return true;
                }
            }

            return false;
        }

        public MeleeWeapon GetTouchingWeapon()
        {
            foreach(KeyValuePair<TriggerDetector, List<Collider>> data in control.COLLIDING_OBJ_DATA.CollidingWeapons)
            {
                MeleeWeapon w = data.Value[0].gameObject.GetComponent<MeleeWeapon>();
                return w;
            }

            return null;
        }
    }
}