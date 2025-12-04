using GestBibli.Objects;
using Nordik_Aventure.Objects.Models.User;
using Nordik_Aventure.Repositories;

namespace Nordik_Aventure.Services;

public class UserService
{
    UserRepository _userRepository;

    public UserService(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public GenericResponse<Employee> GetEmployeeByEmailAndPassword(string email, string password)
    {
        return _userRepository.GetUserByEmailAndPassword(email, password);
    }

    public GenericResponse<Employee> GetEmployeeById(int id)
    {
        return _userRepository.GetEmployeeById(id);
    }
}