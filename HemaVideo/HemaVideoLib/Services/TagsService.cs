using HemaVideoLib.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tortuga.Chain;

namespace HemaVideoLib.Services
{
	public class TagsService
	{
		private readonly SqlServerDataSource m_DataSource;

		public TagsService(SqlServerDataSource dataSource)
		{
			m_DataSource = dataSource ?? throw new ArgumentNullException(nameof(dataSource));
		}

		public Task<List<Weapon>> GetWeaponsAsync()
		{
			return m_DataSource.From("Tags.Weapon").ToCollection<Weapon>().ExecuteAsync();
		}

		public Task<List<Technique>> GetTechniquesAsync()
		{
			return m_DataSource.From("Tags.Technique").ToCollection<Technique>().ExecuteAsync();
		}

		public Task<List<Target>> GetTargetsAsync()
		{
			return m_DataSource.From("Tags.Target").ToCollection<Target>().ExecuteAsync();
		}

		public Task<List<Guard>> GetGuardsAsync()
		{
			return m_DataSource.From("Tags.Guard").ToCollection<Guard>().ExecuteAsync();
		}

		public Task<List<GuardModifer>> GetGuardModifersAsync()
		{
			return m_DataSource.From("Tags.GuardModifer").ToCollection<GuardModifer>().ExecuteAsync();
		}

		public Task<List<Footwork>> GetFootworkAsync()
		{
			return m_DataSource.From("Tags.Footwork").ToCollection<Footwork>().ExecuteAsync();
		}
	}
}