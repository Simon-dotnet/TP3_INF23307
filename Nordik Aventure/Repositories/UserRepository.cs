using GestBibli.Objects;
using Nordik_Aventure.Objects.Models.User;

namespace Nordik_Aventure.Repositories;

public class UserRepository
{
    NordikAventureContext _context = new NordikAventureContext();

    public UserRepository()
    {
        
    }

    public GenericResponse<Employee> GetUserByEmailAndPassword(string email, string password)
    {
        var result = _context.Employees.FirstOrDefault(e => e.EmailAddress == email && e.Password == password);
        if (result == null)
        {
            return new GenericResponse<Employee>("Mauvaises informations", 404);
        }
        
        return new GenericResponse<Employee>(result);
    }
}