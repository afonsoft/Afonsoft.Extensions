using System;
using System.IO;
using System.Reflection;

namespace Afonsoft.Extensions
{
    /// <summary>
    /// Classe para trabalhar com as Assembly
    /// https://stackoverflow.com/questions/1600962/displaying-the-build-date
    /// </summary>
    public static class AssemblyExtensions
    {
        /// <summary>
        /// The method was working for .Net Core 1.0, but stopped working after .Net Core 1.1 release(gives random years in 1900-2020 range)
        /// </summary>
        /// <returns>Displaying the build date</returns>
        /// <remarks>https://blog.codinghorror.com/determining-build-date-the-hard-way/</remarks>
        public static DateTime GetLinkerTime(this Assembly assembly, TimeZoneInfo target = null)
        {
            //https://stackoverflow.com/questions/1600962/displaying-the-build-date
            var filePath = assembly.Location;
            const int c_PeHeaderOffset = 60;
            const int c_LinkerTimestampOffset = 8;

            var buffer = new byte[2048];

            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                stream.Read(buffer, 0, 2048);

            var offset = BitConverter.ToInt32(buffer, c_PeHeaderOffset);
            var secondsSince1970 = BitConverter.ToInt32(buffer, offset + c_LinkerTimestampOffset);
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            var linkTimeUtc = epoch.AddSeconds(secondsSince1970);

            var tz = target ?? TimeZoneInfo.Local;
            var localTime = TimeZoneInfo.ConvertTimeFromUtc(linkTimeUtc, tz);

            return localTime;
        }
    }
}
