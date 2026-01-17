using System.Collections.Generic;
using DormitoryAndCafeteriaSystem.Entities;

namespace DormitoryAndCafeteriaSystem.Repositories.Interfaces
{
    public interface IRoomRepository
    {
        List<RoomEntity> GetAll();
    }
}
