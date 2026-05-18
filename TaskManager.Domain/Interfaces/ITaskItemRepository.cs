using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces.Base;

namespace TaskManager.Domain.Interfaces;

public interface ITaskItemRepository : IRepository<TaskItem>;