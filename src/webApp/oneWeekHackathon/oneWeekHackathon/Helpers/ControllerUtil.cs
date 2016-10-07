using System;
using System.Data.Entity.Spatial;

namespace oneWeekHackathon.Helpers
{
    public static class ControllerUtil
    {
        public static DbGeography CreatePoint(double lat, double lon, int srid = 4326)
        {
            string wkt = String.Format("POINT({0} {1})", lon, lat);

            return DbGeography.PointFromText(wkt, srid);
        }
    }

}