using Udlånssystem_API.Models;

public interface IFabrikatRepository
{
    Task<Fabrikat> GetFabrikatByName(string fabrikatNavn);
    Task Create(Fabrikat fabrikat);
}
