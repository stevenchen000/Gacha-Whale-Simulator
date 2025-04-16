using System;
using Godot;
using StateSystem;

namespace CombatSystem
{
    public partial class CombatRewardNode : CombatStateNode
    {
        [Export] private PackedScene rewardMenuScene;
        [Export] private PackedScene expMenuScene;
        private CombatRewardDisplay rewardMenu;
        private CombatExpDisplay expMenu;
        private StageCompletionData completionData;

        private bool expShown = false;
        private bool isExiting = false;

        protected override void OnStateActivated()
        {
            base.OnStateActivated();
            DisplayRewards();
        }

        private void DisplayRewards()
        {
            rewardMenu = Utils.InstantiateCopy<CombatRewardDisplay>(rewardMenuScene);
            battle.AddChild(rewardMenu);
            completionData = battle.State.CurrentStage.CheckRewards(battle.State);
            rewardMenu.Init(completionData.Rewards);
        }

        private void DisplayExp()
        {
            expMenu = Utils.InstantiateCopy<CombatExpDisplay>(expMenuScene);
            battle.AddChild(expMenu);
            expMenu.Init(completionData.LevelUps);
        }

        protected override void RunState(double delta)
        {
            if (!expShown && !IsInstanceValid(rewardMenu))
            {
                expShown = true;
                rewardMenu = null;
                DisplayExp();
                Utils.Print(this, "Showing exp animation");
            }
            if (expShown && !IsInstanceValid(expMenu) && !isExiting)
            {
                isExiting = true;
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
