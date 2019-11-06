public class Gun
{
    public Gun(float reloadTime, float fireForce)
    {
        _reloadTime = reloadTime;
        _fireForce = fireForce;
    }

    public float GetReloadTime() => _reloadTime;
    public float GetFireForce() => _fireForce;

    private float _reloadTime; // In seconds
    private float _fireForce; // Velocity applied when bullet is spawn

    public static Gun handgun = new Gun(1f, 50f);
}
