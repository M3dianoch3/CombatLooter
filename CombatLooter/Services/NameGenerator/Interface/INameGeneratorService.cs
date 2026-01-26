using CombatLooter.Classes.Interface;

namespace CombatLooter.Services.NameGeneratorService.Interface
{
    public interface INameGeneratorService
    {
        string GenerateName(IItem item);
        string GenerateName(IWeapon weapon);
        string GenerateName(IArmor armor);
    }
}
