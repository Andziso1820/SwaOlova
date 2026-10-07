namespace SwaOlova.Infrastructure.Service.Deliveries;

public interface IDeliveryService
{
    void StartDelivery(Guid deliveryId);

    void CompleteDelivery(Guid deliveryId);

    string GenerateOtp();

    bool ValidateOtp(string otp, string expectedOtp);

    void RecordProofOfDelivery(Guid deliveryId, string proofOfDeliveryUrl);
}