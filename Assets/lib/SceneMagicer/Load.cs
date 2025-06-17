using SceneMagicer;
using System.Collections.Generic;
using MathsAndSome;
using UnityEngine;

namespace SceneMagicer.Load
{
    public static class Loading
    {
        public static void ResetPlayer(Magicer.Checkpoints.Checkpoint cp)
        {
            PlayerController pc = mas.player.GetPlayer();
            CombatController cc = mas.player.GetCombatController();

            // Combat
            cc.StopAllCoroutines();
            cc.health = 100;
            cc.canAttack = true;
            cc.canSwitchWeapon = true;

            // Movement
            pc.s = PlayerStates.state.walking;
            pc.stamina = pc.maxStamina;
            pc.canJump = true;
            pc.canDash = true;
            pc.dashing = false;

            pc.gameObject.transform.position = cp.spawnPos;
            pc.rb.linearVelocity = Vector3.zero;
        }

        public static void ReLoadLevel(Magicer.Checkpoints.Checkpoint cp)
        {
            ResetPlayer(cp);

            foreach (BaseEnemy be in LevelData.enemies)
            {

            }
        }
    }
}
