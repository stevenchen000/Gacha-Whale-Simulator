using System;
using Godot;
using StateSystem;

namespace CombatSystem
{
    public partial class CombatVictoryNode : CombatStateNode
    {
        private SimpleWeakRef<MainGame> menu;

        protected override void OnStateActivated()
        {
            base.OnStateActivated();
            var tempGame = Utils.FindParentOfType<MainGame>(this);
            menu = new SimpleWeakRef<MainGame>(tempGame);

            AddRewards();
            
            Utils.Print(this, "You win!");
        }

        private void AddRewards()
        {
            var state = battle.State;
            var stage = state.CurrentStage;

            int staminaCost = stage.StaminaCost;
            var completionData = stage.CheckRewards(state);

            var rewards = completionData.Rewards;
            foreach(var reward in rewards)
            {
                reward.ReceiveReward();
            }

            int exp = completionData.Exp;
            AddExpToParty(exp);

            GameState.UseStamina(staminaCost);
        }

        private void AddExpToParty(int exp)
        {
            var party = battle.State.PlayerParty;
            var partyMembers = party.GetAllMembers();

            foreach(var member in partyMembers)
            {
                var character = member.Character;
                character.AddExp(exp);
            }
        }

        protected override void RunState(double delta)
        {
            if(timeInState > 5)
            {
                Utils.Print(this, "Don't forget to add rewards");
                battle.ReturnToCombatMenu();
            }
        }

        protected override StateNode CheckStateChange()
        {
            StateNode result = null;

            return result;
        }

        protected override void OnStateDeactivated()
        {
            
        }
    }
}
