using Dapper;
using Dapper.Contrib.Extensions;
using MachineManagement.Portal.Domain.Entities;
using MachineManagement.Portal.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MachineManagement.Portal.Repository
{
    public class VirtualMachineRepository : IReadWriteRepository<VirtualMachine>
    {
        private readonly string _connectionString;

        public VirtualMachineRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(VirtualMachine entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                connection.Insert(entity);
            }
        }

        public void Delete(VirtualMachine entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                connection.Delete(entity);
            }
        }

        public void Edit(VirtualMachine entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                connection.Update(entity);
            }
        }

        public async Task<IEnumerable<VirtualMachine>> GetAllAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM VirtualMachines";
                return connection.QueryAsync<VirtualMachine>(sql).Result.ToList();
            }
        }

        public Task<VirtualMachine> GetByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM VirtualMachines WHERE Id = @Id";
                return Task.FromResult(connection.QueryAsync<VirtualMachine>(sql, new { id }).Result.FirstOrDefault());
            }
        }
    }
}
