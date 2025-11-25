using GestBibli.Objects;
using Nordik_Aventure.Objects.Models;
using Nordik_Aventure.Objects.Models.Finance;
using Nordik_Aventure.Repositories;

namespace Nordik_Aventure.Services;

public class MovementHistoryService
{
    private MovementHistoryRepository _movementHistoryRepository;
    
    public MovementHistoryService(MovementHistoryRepository movementHistoryRepository)
    {
        _movementHistoryRepository = movementHistoryRepository;
    }

    public GenericResponse<List<MovementHistory>> GetLast30MovementHistory()
    {
        return _movementHistoryRepository.GetLast30MovementHistory();
    }

    public GenericResponse<MovementHistory> AddEnteringMovementHistory(MovementHistory movementHistory)
    {
        return _movementHistoryRepository.AddEnteringMovementStock(movementHistory);
    }
    
    public GenericResponse<MovementHistory> AddLeavingMovementHistory(MovementHistory movementHistory)
    {
        return _movementHistoryRepository.AddLeavingMovementStock(movementHistory);
    }
}