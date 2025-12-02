using GestBibli.Objects;
using Nordik_Aventure.Controllers;
using Nordik_Aventure.Objects.Models;
using Nordik_Aventure.Repositories;

namespace Nordik_Aventure.Services;

public class ClientInteractionsService
{
    private readonly ClientInteractionsRepository _clientInteractionsRepository;

    public ClientInteractionsService(ClientInteractionsRepository clientInteractionsRepository)
    {
        _clientInteractionsRepository = clientInteractionsRepository;
    }

    public GenericResponse<List<ClientInterraction>> GetClientInterractionsByClient(int clientId)
    {
        return _clientInteractionsRepository.GetClientInterractionByClientId(clientId);
    }

    public GenericResponse<ClientInterraction> AddClientInteraction(ClientInterraction clientInterraction)
    {
        return _clientInteractionsRepository.AddClientInteraction(clientInterraction);
    }
}