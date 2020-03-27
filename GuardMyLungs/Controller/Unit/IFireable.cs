namespace Controller
{
    public interface IFireable
    {
        long WeaponId { set; }
        void Fire();
    }
}
