namespace SwaOlova.Infrastructure.Service.Common;

public static class GeoCalculator
{
    private const double EarthRadiusKm = 6371d;

    public static decimal CalculateDistanceKm(decimal startLatitude, decimal startLongitude, decimal endLatitude, decimal endLongitude)
    {
        var startLatRad = ToRadians(startLatitude);
        var endLatRad = ToRadians(endLatitude);
        var deltaLat = ToRadians(endLatitude - startLatitude);
        var deltaLon = ToRadians(endLongitude - startLongitude);

        var a = Math.Pow(Math.Sin(deltaLat / 2), 2)
            + Math.Cos(startLatRad) * Math.Cos(endLatRad) * Math.Pow(Math.Sin(deltaLon / 2), 2);
        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        return (decimal)(EarthRadiusKm * c);
    }

    public static decimal CalculateBearing(decimal startLatitude, decimal startLongitude, decimal endLatitude, decimal endLongitude)
    {
        var startLatRad = ToRadians(startLatitude);
        var endLatRad = ToRadians(endLatitude);
        var deltaLon = ToRadians(endLongitude - startLongitude);

        var y = Math.Sin(deltaLon) * Math.Cos(endLatRad);
        var x = Math.Cos(startLatRad) * Math.Sin(endLatRad)
            - Math.Sin(startLatRad) * Math.Cos(endLatRad) * Math.Cos(deltaLon);

        var bearing = ToDegrees(Math.Atan2(y, x));
        return (decimal)((bearing + 360d) % 360d);
    }

    public static int CalculateTravelTime(decimal distanceKm, decimal averageSpeedKmh = 40m)
    {
        if (distanceKm <= 0 || averageSpeedKmh <= 0)
        {
            return 0;
        }

        return (int)Math.Ceiling(distanceKm / averageSpeedKmh * 60m);
    }

    private static double ToRadians(decimal value) => (double)value * Math.PI / 180d;

    private static double ToDegrees(double value) => value * 180d / Math.PI;
}