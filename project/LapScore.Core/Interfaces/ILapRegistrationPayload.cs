using System;
namespace LapScore.Core.Interfaces
{
    interface ILapRegistrationPayload
    {
        int CarNumber { get; }
        string TransponderNumber { get; }
        decimal LapRegistrationElapsedTime{get;}
    }
}
