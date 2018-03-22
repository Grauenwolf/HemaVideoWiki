using HemaVideoLib.Models;
using System;
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
        public async Task AddVideo(IUser applicationUser, NewVideo video)
        {
            if (applicationUser == null)
                throw new ArgumentNullException(nameof(applicationUser), $"{nameof(applicationUser)} is null.");

            if (video == null)
                throw new ArgumentNullException(nameof(video), $"{nameof(video)} is null.");

            //https://www.youtube.com/watch?v=6ISOK-XtvYs

            if (video.Url.Contains("www.youtube.com"))
            {
                var uri = new Uri(video.Url);
                var parts = HttpUtility.ParseQueryString(uri.Query);
                video.VideoServiceVideoId = parts["v"];
                video.VideoServiceKey = 1;
            }
            else
            {
                video.VideoServiceKey = 0;
            }



            await m_DataSource.WithUser(applicationUser).Insert(video).ExecuteAsync();
        }

    }
}
