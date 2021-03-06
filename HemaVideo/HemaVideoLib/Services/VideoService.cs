﻿using HemaVideoLib.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Tortuga.Chain;

namespace HemaVideoLib.Services
{
	public class VideoService : ServiceBase
	{
		public VideoService(SqlServerDataSource dataSource) : base(dataSource)
		{
		}

		public async Task<int> AddVideo(NewVideo video, IUser currentUser)
		{
			if (video == null)
				throw new ArgumentNullException(nameof(video), $"{nameof(video)} is null.");

			//https://www.youtube.com/watch?v=6ISOK-XtvYs

			if (video.Url.Contains("www.youtube.com") || video.Url.Contains("youtu.be"))
			{
				var uri = new Uri(video.Url);
				var query = HttpUtility.ParseQueryString(uri.Query);

				if (query.AllKeys.Contains("v"))
				{
					video.VideoServiceVideoId = query["v"];
				}
				else
				{
					video.VideoServiceVideoId = uri.Segments.Last();
				}

				video.VideoServiceKey = 1;
			}
			else
			{
				video.VideoServiceKey = 0;
			}

			if (string.IsNullOrWhiteSpace(video.Description))
				video.Description = null;
			if (string.IsNullOrWhiteSpace(video.Url))
				video.Url = null;
			if (string.IsNullOrWhiteSpace(video.Author))
				video.Author = null;

			return await DataSource(currentUser).Insert(video).ToInt32().ExecuteAsync();
		}

		public async Task UpdateVideoAsync(UpdatedVideo video, IUser currentUser)
		{
			await CheckPermissionVideoAsync(video.VideoKey, currentUser);
			await DataSource(currentUser).Update("Interpretations.Video", video).ExecuteAsync();
		}
	}
}