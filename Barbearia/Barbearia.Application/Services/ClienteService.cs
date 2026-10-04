using Barbearia.Application.DTOs;
using Barbearia.Application.Interfaces;
using Barbearia.Domain.Entities;
using Barbearia.Domain.Enums;
using Barbearia.Domain.Interfaces;


namespace Barbearia.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repository;
        private readonly ITelefoneRepository _telefoneRepository;

        public ClienteService(IClienteRepository repository, ITelefoneRepository telefoneRepository)
        {
            _repository = repository;
            _telefoneRepository = telefoneRepository;
        }

        public async Task<IEnumerable<ClienteDTO>> GetAllAsync()
        {
            var clientes = await _repository.GetAllAsync();
            return clientes.Select(c => new ClienteDTO
            {
                ID_Cli = c.ID_Cli,
                Nome = c.Nome,
                Status = c.Status.ToString(),
                Observacao = c.Observacao,
                Telefones = c.Telefones.Select(t => t.NumeroTel).ToList()
            });
        }

        public async Task<ClienteDTO?> GetByIdAsync(int id)
        {
            var cliente = await _repository.GetByIdAsync(id);
            if (cliente == null) return null;

            return new ClienteDTO
            {
                ID_Cli = cliente.ID_Cli,
                Nome = cliente.Nome,
                Status = cliente.Status.ToString(),
                Observacao = cliente.Observacao,
                Telefones = cliente.Telefones.Select(t => t.NumeroTel).ToList()
            };
        }

        public async Task<IEnumerable<TelefoneDTO>> GetTelefonesAsync(int clienteId)
        {
            var telefones = await _telefoneRepository.GetByClienteIdAsync(clienteId);
            return telefones.Select(t => new TelefoneDTO { ID_Tel = t.ID_Tel, NumeroTel = t.NumeroTel });
        }

        public async Task<ClienteDTO> CreateAsync(CreateClienteDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nome))
                throw new ArgumentException("Nome é obrigatório.");

            var cliente = new Cliente
            {
                Nome = dto.Nome,
                Observacao = dto.Observacao,
                Status = StatusAtivo.Ativo.ToString().ToLower() 
            };

            await _repository.AddAsync(cliente);

            return new ClienteDTO
            {
                ID_Cli = cliente.ID_Cli,
                Nome = cliente.Nome,
                Status = cliente.Status,
                Observacao = cliente.Observacao,
                Telefones = new List<string>()
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdateClienteDTO dto)
        {
            var cliente = await _repository.GetByIdAsync(id);
            if (cliente == null) return false;

            if (string.IsNullOrWhiteSpace(dto.Nome))
                throw new ArgumentException("Nome é obrigatório.");

            cliente.Nome = dto.Nome;
            cliente.Observacao = dto.Observacao;

            await _repository.UpdateAsync(cliente);
            return true;
        }

        public async Task<bool> InativarAsync(int id)
        {
            var cliente = await _repository.GetByIdAsync(id);
            if (cliente == null) return false;

            cliente.Status = StatusAtivo.Inativo.ToString().ToLower();
            await _repository.UpdateAsync(cliente);
            return true;
        }

        public async Task<bool> AddTelefoneAsync(int clienteId, string numero)
        {
            var cliente = await _repository.GetByIdAsync(clienteId);
            if (cliente == null) return false;

            await _telefoneRepository.AddAsync(new Telefone
            {
                NumeroTel = numero,
                ID_Cli_FK = clienteId
            });

            return true;
        }
        public async Task<bool> UpdateTelefoneAsync(int telefoneId, string numero)
        {
            var telefone = await _telefoneRepository.GetByIdAsync(telefoneId);
            if (telefone == null) return false;

            telefone.NumeroTel = numero;
            await _telefoneRepository.UpdateAsync(telefone);
            return true;
        }

        public async Task<bool> RemoveTelefoneAsync(int telefoneId)
        {
            var telefone = await _telefoneRepository.GetByIdAsync(telefoneId);
            if (telefone == null) return false;

            await _telefoneRepository.DeleteAsync(telefoneId);
            return true;
        }
    }
}