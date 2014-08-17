using System;
namespace LapScore.Core.Interfaces
{
    interface ILapRegistrationPayload
    {
        int CarNumber { get; }
        DateTime DateTimeStampUTC { get; }
        string TransponderNumber { get; }
    }
}
