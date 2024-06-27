using RSAllies.Shared.HelperTypes;
using RSAllies.Venues.Data;
using RSAllies.Venues.Entities;
using System.Security.Cryptography;

namespace RSAllies.Venues.Services
{
    internal class PaymentService
    {
        private static readonly RandomNumberGenerator rng = RandomNumberGenerator.Create();

        internal static string GeneratePaymentNumber(VenueDbContext context, Guid userId)
        {
            // Generate a random number
            byte[] randomNumber = new byte[4]; // 4 bytes for a 32-bit integer
            rng.GetBytes(randomNumber);

            // Convert the byte array to an integer
            int result = BitConverter.ToInt32(randomNumber, 0);

            // make the result a positive number
            result = Math.Abs(result);

            string paymentNumber = $"PN-{result}";

            // Check if the payment number already exists
            while (context.Payments.Any(p => p.PaymentNumber == paymentNumber))
            {
                // Generate a new random number
                rng.GetBytes(randomNumber);
                result = BitConverter.ToInt32(randomNumber, 0);
                paymentNumber = $"PN-{result}";
            }

            // Add the payment to the database
            context.Payments.Add(new Payment
            {
                UserId = userId,
                PaymentNumber = paymentNumber,
                Status = PaymentStatus.Pending,
                RequestedDate = DateTime.UtcNow
            });

            context.SaveChanges();

            return paymentNumber;
        }

        internal static Result ProcessPayment(VenueDbContext context, string paymentNumber)
        {
            var payment = context.Payments
                .FirstOrDefault(p => p.PaymentNumber == paymentNumber);

            if (payment == null)
            {
                var result = new Result(false, new Error("Payment Failed", "Invalid payment number"));
                return result;
            }

            if (payment.Status == PaymentStatus.Paid)
            {
                var result = new Result(false, new Error("Payment Failed", "Payment has already been processed"));
                return result;
            }

            // Simulate processing the payment
            payment.Status = PaymentStatus.Paid;
            payment.PaymentDate = DateTime.UtcNow;

            context.SaveChanges();

            return new Result(true, Error.None);
        }
    }
}
