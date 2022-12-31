public class TestPassive : AnimalPassive
{

    public override void SubscribePassive()
    {
        host.DamageDealer.OnHit.AddListener(DamageBuffVSDesert);
    }

    private void DamageBuffVSDesert(Damageable damageAble, AnimalAttack givenAttack)
    {
        if (damageAble.RefAnimal.Habitat == Habitat.Desert)
        {
            givenAttack.Damage.AddMod(1.3f);
        }
    }


}
