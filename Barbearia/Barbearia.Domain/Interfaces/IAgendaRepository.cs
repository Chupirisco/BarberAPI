using Barbearia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Barbearia.Domain.Interfaces
{
    public interface IAgendaRepository
    {
        Task<Agenda?> GetByIdAsync(int id);
        Task<IEnumerable<Agenda>> GetAllAsync();

        // agenda de um dia específico (útil pra tela de agenda do dia)
        Task<IEnumerable<Agenda>> GetByDataAsync(DateTime data);

        // clientes com conta pendente: status Concluído + pagamento Pendente
        Task<IEnumerable<Agenda>> GetContasReceberAsync();

        Task AddAsync(Agenda agenda);
        Task UpdateAsync(Agenda agenda);
        Task DeleteAsync(int id);
    }
}
