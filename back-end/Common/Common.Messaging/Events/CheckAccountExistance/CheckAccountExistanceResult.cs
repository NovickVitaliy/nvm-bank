namespace Common.Messaging.Events.CheckAccountExistance;

public class CheckAccountExistanceResult {
    public bool Exists { get; init; }

    public CheckAccountExistanceResult(bool exists) {
        Exists = exists;
    }
}