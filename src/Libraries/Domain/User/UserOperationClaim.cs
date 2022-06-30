namespace Domain.User;

public class UserOperationClaim
{
    public int UserId { get; set; }
    public User User { get; set; }
    public int OperationClaimId { get; set; }
    public OperationClaim OperationClaim { get; set; }
}