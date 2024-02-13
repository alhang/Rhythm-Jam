public class HealthPotion : Drop
{
	public float healAmount;

    protected override void Pickup(Player player)
    {
        player.Heal(healAmount);
        Destroy(gameObject);
    }
}

