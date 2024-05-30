namespace Users.API.Interfaces;

public interface ISoftDelete
{
    bool IsDeleted { get; set; }
}