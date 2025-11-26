using GestBibli.Objects;
using Nordik_Aventure.Objects.Models.User;
using Nordik_Aventure.Repositories;

namespace Nordik_Aventure.Services;

public class ClientService
{
    private readonly ClientRepository _clientRepository;

    public ClientService(ClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public GenericResponse<Client> CreateClient(Client client)
    {
        return _clientRepository.CreateClient(client);
    }

    public GenericResponse<Client> GetClientById(int id)
    {
        return _clientRepository.GetClientById(id);
    }

    public GenericResponse<List<Client>> GetAllClients()
    {
        return _clientRepository.GetAllClients();
    }
}