﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class PlayerAttack : SubComponent
    {
        public override void InitComponent()
        {
            subComponentProcessor.ArrSubComponents[(int)SubComponentType.PLAYER_ATTACK] = this;
        }

        public override void OnFixedUpdate()
        {
            throw new System.NotImplementedException();
        }

        public override void OnUpdate()
        {

            if (control.Attack)
            {
                if (control.ATTACK_DATA.AttackButtonIsReset)
                {
                    control.ATTACK_DATA.AttackTriggered = true;
                    control.ATTACK_DATA.AttackButtonIsReset = false;
                }
            }
            else
            {
                control.ATTACK_DATA.AttackButtonIsReset = true;
                control.ATTACK_DATA.AttackTriggered = false;
            }
        }
    }
}