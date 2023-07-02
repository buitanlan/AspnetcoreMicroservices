namespace Contracts.Domains.Interfaces;

public interface IUserTracking
{
    string CreateBy { get; set; }
    string? LastModifiedBy { get; set; }
}