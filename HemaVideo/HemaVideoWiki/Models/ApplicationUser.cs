﻿using HemaVideoLib.Models;
using Microsoft.AspNetCore.Identity;

namespace HemaVideoWiki.Models
{
	// Add profile data for application users by adding properties to the ApplicationUser class
	public class ApplicationUser : IdentityUser, IUser
	{
		public string DisplayName { get; set; }

		public int? UserKey { get; set; }
	}
}