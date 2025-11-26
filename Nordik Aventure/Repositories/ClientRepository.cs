using GestBibli.Objects;
using Nordik_Aventure.Objects.Models.User;

namespace Nordik_Aventure.Repositories;

public class ClientRepository
{
    NordikAventureContext _context = new NordikAventureContext();

    public ClientRepository()
    {
    }

    public GenericResponse<List<Client>> GetAllClients()
    {
        try
        {
            var result = _context.Clients.ToList();
            return new GenericResponse<List<Client>>(result);
        }
        catch (Exception ex)
        {
            return new GenericResponse<List<Client>>("Erreur lors du get de tout les clients", 500);
        }
    }

    public GenericResponse<Client> GetClientById(int id)
    {
        try
        {
            var result = _context.Clients.Find(id);
            if (result == null)
            {
                return new GenericResponse<Client>("Client n'existe pas avec cet Id", 404);
            }

            return new GenericResponse<Client>(result);
        }
        catch (Exception ex)
        {
            return new GenericResponse<Client>("Erreur lors du get du client", 500);
        }
    }

    public GenericResponse<Client> CreateClient(Client client)
    {
        try
        {
            _context.Clients.Add(client);
            _context.SaveChanges();
            return new GenericResponse<Client>(client);
        }
        catch (Exception ex)
        {
            return new GenericResponse<Client>("Erreur lors du add client", 500);
        }
    }
}