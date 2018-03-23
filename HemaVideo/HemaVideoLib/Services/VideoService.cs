using HemaVideoLib.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Tortuga.Chain;

namespace HemaVideoLib.Services
{
    public class VideoService
    {
        readonly SqlServerDataSource m_DataSource;

        public VideoService(SqlServerDataSource dataSource)
        {
            m_DataSource = dataSource ?? throw new ArgumentNullException(nameof(dataSource));
        }
        public async Task<int> AddVideo(IUser applicationUser, NewVideo video)
        {
            if (applicationUser == null)
                throw new ArgumentNullException(nameof(applicationUser), $"{nameof(applicationUser)} is null.");

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

            return await m_DataSource.WithUser(applicationUser).Insert(video).ToInt32().ExecuteAsync();
        }

    }
}
