using System;
using System.Collections.Generic;
using Godot;
using StateSystem;

namespace CombatSystem
{
    public partial class CombatKnockbackState : CombatStateNode
    {
        [Export] private CombatStateNode turnFinishedNode;
        private List<KnockbackCollisionData> knockbacks;
        [Export] private double knockbackTime = 0.5;

        /// <summary>
        /// target was knocked into their ally
        /// </summary>
        private List<KnockbackCollisionData> allyKnockbacks = new List<KnockbackCollisionData>();
        /// <summary>
        /// target was knocked into their enemy
        /// </summary>
        private List<KnockbackCollisionData> enemyKnockbacks = new List<KnockbackCollisionData>();

        private int enemyKnockbackIndex = 0;

        private TimeHandler time = new TimeHandler();

        private KnockbackCollisionData currKnockback;

        private BattleCharacter target;
        private BattleCharacter obstacle;


        protected override void OnStateActivated()
        {
            base.OnStateActivated();
            knockbacks = battle.RunKnockbacks();
            SeparateKnockbacks();
            time.Reset();
        }

        protected override void RunState(double delta)
        {
            time.Tick(delta);

            if (time.TimeReached(0.2))
            {
                PlayKnockbackAnimations();
            }


            if (time.TimeReached(0.5))
            {
                foreach (var knockback in knockbacks)
                {
                    DealKnockbackDamage(knockback, battle.turnData);
                }
            }
        }

        protected override StateNode CheckStateChange()
        {
            StateNode result = null;

            if (knockbacks.Count == 0 || time.TimePassed(1))
            {
                result = turnFinishedNode;
            }

            return result;
        }

        protected override void OnStateDeactivated()
        {
            knockbacks = null;
            allyKnockbacks.Clear();
            enemyKnockbacks.Clear();
        }


        /*****************
         * Helpers
         * *************/

        private void SeparateKnockbacks()
        {
            for (int i = 0; i < knockbacks.Count; i++)
            {
                var knockback = knockbacks[i];
                if(knockback.ObstacleIsEnemy())
                    enemyKnockbacks.Add(knockback);
                else
                    allyKnockbacks.Add(knockback);
            }
        }

        private void PlayKnockbackAnimations()
        {
            foreach(var knockback in knockbacks)
            {
                var character = knockback.KnockedbackCharacter;
                var direction = knockback.KnockbackDirection;

                switch (direction)
                {
                    case CharacterDirection.UP:
                        character.PlayAnimation("knock_up");
                        break;
                    case CharacterDirection.DOWN:
                        character.PlayAnimation("knock_down");
                        break;
                    case CharacterDirection.LEFT:
                        character.PlayAnimation("knock_left");
                        break;
                    case CharacterDirection.RIGHT:
                        character.PlayAnimation("knock_right");
                        break;
                }
            }
        }


        private void MoveCameraBetweenTargets()
        {
            var middle = (target.PositionFollower.Position + obstacle.PositionFollower.Position) / 2;
            battle.Camera.MoveTowardsPosition(middle);
        }

        private void DealKnockbackDamage(KnockbackCollisionData collision, TurnData data)
        {
            var target = collision.KnockedbackCharacter;
            var obstacle = collision.KnockedIntoCharacter;

            int damage = data.GetDamageTaken(target);
            int spacesRemaining = collision.NumberOfSpacesLeft;
            int knockbackDamage = (int)(damage * 0.10 * spacesRemaining);
            if (collision.ObstacleIsEnemy())
            {
                target.TakeHpDamage(knockbackDamage);
            }
            else
            {
                target.TakeHpDamage(knockbackDamage);
                obstacle.TakeHpDamage(knockbackDamage);
            }
        }
    }
}
