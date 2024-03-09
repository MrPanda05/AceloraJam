using Godot;
using System;

namespace Commons.Components
{
    public partial class HealthComponent : Node2D
    {
        [Export] public float health;
        [Export] private float maxHealth;

        [Export] private bool beDisable = false;

        public Action OnDeath;
        public void DisableMe(bool value)
        {
            Node2D parent = (Node2D)GetParent();
            parent.ProcessMode = ProcessModeEnum.Disabled;
            parent.Visible = value;
        }
        private void Death()
        {
            if(!beDisable)
            {
                GD.Print("Got queuefree");
                GetParent().QueueFree();
                return;
            }
            GD.Print("Got Disable");
            CallDeferred(HealthComponent.MethodName.DisableMe, false);
            return;
        }
        public void Damage(int amount = 1)
        {
            health -= amount;
            if (health <= 0)
            {
                GD.Print("Death");
                Death();
                OnDeath?.Invoke();
            }
        }
        public void SetIfWantsDisable(bool value)
        {
            beDisable = value;
        }

    }
}
