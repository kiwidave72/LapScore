using System;
namespace LapScore.Core.Message.Payload
{
    interface ILapRegistrationPayload
    {
        int CarNumber { get; }
        DateTime DateTimeStampUTC { get; }
        string TransponderNumber { get; }
    }
}
