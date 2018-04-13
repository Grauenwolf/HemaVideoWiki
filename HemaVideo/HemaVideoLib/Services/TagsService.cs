﻿using HemaVideoLib.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tortuga.Chain;

namespace HemaVideoLib.Services
{
	public class TagsService : ServiceBase
	{
		public TagsService(SqlServerDataSource dataSource) : base(dataSource)
		{
		}

		public Task<List<Weapon>> GetWeaponsAsync(IUser currentUser)
		{
			return DataSource(currentUser).From("Tags.Weapon").ToCollection<Weapon>().ExecuteAsync();
		}

		public Task<List<Technique>> GetTechniquesAsync(IUser currentUser)
		{
			return DataSource(currentUser).From("Tags.Technique").ToCollection<Technique>().ExecuteAsync();
		}

		public Task<List<Target>> GetTargetsAsync(IUser currentUser)
		{
			return DataSource(currentUser).From("Tags.Target").ToCollection<Target>().ExecuteAsync();
		}

		public Task<List<Guard>> GetGuardsAsync(IUser currentUser)
		{
			return DataSource(currentUser).From("Tags.Guard").ToCollection<Guard>().ExecuteAsync();
		}

		public Task<List<GuardModifer>> GetGuardModifersAsync(IUser currentUser)
		{
			return DataSource(currentUser).From("Tags.GuardModifer").ToCollection<GuardModifer>().ExecuteAsync();
		}

		public Task<List<Footwork>> GetFootworkAsync(IUser currentUser)
		{
			return DataSource(currentUser).From("Tags.Footwork").ToCollection<Footwork>().ExecuteAsync();
		}
	}
}